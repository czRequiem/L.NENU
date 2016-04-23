using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//引入的命名空间
using Castle.ActiveRecord;
using NHibernate.Criterion;
using NHibernate;

namespace L.NENU.Manager
{
    /// <summary>
    /// ManagerBase 是泛型类 所有数据访问类的基本类
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class ManagerBase<T> : ActiveRecordBase<T> where T : class
    {
        #region   数据库数据的增删改查

        /// <summary>
        /// 传入一个对象  查询数据库中该实体的所有数据实体
        /// </summary>
        /// <returns>该对象的所有实体</returns>
        public IList<T> GetAll()
        {

            //查询数据库返回该实体的所有结果数组 并转化为ILIst 集合
            return FindAll(typeof(T)) as IList<T>;

            //  IList  与List 有什么不同
            //此处严格注意 Ilist 不能as 为List 数据返回为Null  应用  tolist 转换实现对list数据的操作

            // protected internal static Array FindAll(Type targetType);
            // 摘要:
            //     Returns all instances found for the specified type.
            //     找到指定的类型的所有实例。
            // 参数:
            //   targetType: The target type.目标类型
            //
            // 返回结果:
            //     The System.Array of results : 结果数组
        }


        /// <summary>
        /// 隐藏 可以被重写 创建单个实体
        /// </summary>
        /// <param name="t">要创建的实体对象</param>
        public new void Create(T t)
        {
            //
            ActiveRecordBase.Create(t);

            // protected internal static void Create(object instance);
            // 摘要:
            //     Creates (Saves) a new instance to the database.
            //      创建 （保存） 到数据库的新实例。
            // 参数:
            //   instance: 实例
            //     The ActiveRecord instance to be created on the database
            //      要在数据库中创建的活动记录实例
        }


        ///// <summary>
        ///// 利用主键 id 删除数据
        ///// </summary>
        ///// <param name="id">数据ID</param>
        //public void Delete(int id)
        //{
        //    ActiveRecordBase.Delete(id);
        //}   该方法已经被弃用

        /// <summary>
        /// 利用该数据的实体 删除数据库中对应的数据
        /// </summary>
        /// <param name="t">数据实体</param>
        public new void Delete(T t)
        {
            ActiveRecordBase.Delete(t);
        }


        /// <summary>
        /// 隐藏 修改实体的方法  可以被重写
        /// </summary>
        /// <param name="t">覆盖的实体</param>
        public new void Update(T t)
        {
            ActiveRecordBase.Update(t);
        }

        /// <summary>
        /// 根据对象ID查询数据库中对应的数据
        /// </summary>
        /// <param name="id">对象的ID 主键</param>
        /// <returns>对应ID的对象</returns>
        public T Get(int id)
        {
            return FindByPrimaryKey(typeof(T), id) as T;
        }


        /// <summary>
        /// 通用方法  实现传入对象对数据库进行查询  
        /// </summary>
        /// <param name="queryConditions">传入的对象</param>
        /// <returns>返回为Null为未查询到对应数据 查询到 即返回对应数据</returns>
        public T Get(IList<ICriterion> queryConditions)
        {
            //查询满足条件的一条数据 FindOne，T是泛型，UserManager（UserManager:ManagerBase<User>） 的对象调用 ManagerBase的方法时时，T就是User对象
            object obj = ActiveRecordBase.FindOne(typeof(T), queryConditions.ToArray());

            if (obj == null) return null;  //当查询出来的对象为空的时候  即返回空值  通用方法

            else return obj as T;
        }

        public IList<T> GetAll(IList<ICriterion> queryConditions)//根据查询条件集合获取所有对象
        {
            Array arr = FindAll(typeof(T));//Castle.net默认是数组
            return arr as IList<T>;//将数组转换为IList集合
        }


        #endregion


        #region  判断数据是否存在
        public bool Exists(int id)
        {
            return ActiveRecordBase.Exists(typeof(T), id);
        }

        public bool Exists(IList<ICriterion> queryConditions)
        {
            return ActiveRecordBase.Exists(typeof(T), queryConditions.ToArray());
        }
        #endregion


        #region  分页查询控件调用
        /// <summary>
        /// 根据分页查询相应的对象集合
        /// </summary>
        /// <param name="queryConditions"></param>
        /// <param name="orderList"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<T> GetPaged(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count)
        {
            if (queryConditions == null)  //如果为空则赋值一个总数为0的集合
            {
                queryConditions = new List<ICriterion>();
            }
            if (orderList == null)   //如果为空则赋值一个总数为0的集合
            {
                orderList = new List<Order>();
            }
            count = Count(typeof(T), queryConditions.ToArray());  //根据查询条件获取满足条件的对象总数

            //根据分页条件进行对象查询
            Array arr = SlicedFindAll(typeof(T), (pageIndex - 1) * pageSize, pageSize, orderList.ToArray(), queryConditions.ToArray());

            return arr as IList<T>;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryConditions"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<T> GetPaged(IList<QueryConditions> queryConditions, int pageIndex, int pageSize, out int count)
        {
            //实例化一个hql查询语句对象
            StringBuilder hql = new StringBuilder(@"from" + typeof(T).Name + "d");

           
            //根据查询条件构造hql查询语句
            for (int i = 0; i < queryConditions.Count; i++)
            {
                QueryConditions qc = queryConditions[i];  //获取当前序号对应的条件
                if (qc.Value != null)
                {
                    AddHqlSatements(hql);  //增加where或者and语句
                    hql.Append(string.Format("d.{0} {1} :q_{2}", qc.PropertyName, qc.Operator, i));
                }
            }
            
            ISession session = ActiveRecordBase.holder.CreateSession(typeof(T));  //获取管理T的session对象
            IQuery query = session.CreateQuery(hql.ToString());  //获取满足条件的数据
            IQuery queryScalar = session.CreateQuery("select count(ID)" + hql.ToString());  //获取满足条件的数据总数

            for (int i = 0; i < queryConditions.Count; i++)
            {
                QueryConditions qc = queryConditions[i]; //获取当前序列号对应的条件


                if (qc.Value != null)
                {
                    //如果四like语句 ,则修改表达方式
                    if (qc.Operator.ToUpper() == "LIKE")
                    {
                        qc.Value = "%" + qc.Value + "%";
                    }

                    //用查询条件的值去填充hql
                    queryScalar.SetParameter("q_" + i, qc.Value);
                    query.SetParameter("q_" + i, qc.Value);
                }

                
            }

            IList<object> result = queryScalar.List<object>(); //执行查询条件总数的查询对象
            int.TryParse(result[0].ToString(), out count);
            query.SetFirstResult((pageIndex - 1) * pageSize);
            query.SetMaxResults(pageSize);
            IList<T> arr = query.List<T>();

            //session.Close();  此处不要显示关闭

            return arr;
        }


        /// <summary>
        /// 构建hql语句
        /// </summary>
        /// <param name="hql">HQL语句</param>
        private void AddHqlSatements(StringBuilder hql)
        {
            if (!hql.ToString().Contains("where"))  //判断是否存在where
            {
                hql.Append("where");  //如果不存在则添加where
            }
            else
            {
                hql.Append("and");  //如果存在 即在后面添加and
            }
        }
        #endregion

    }



    /// <summary>
    /// 
    /// </summary>
    public class QueryConditions
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 操作符号
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public object Value { get; set; }
    }
}
