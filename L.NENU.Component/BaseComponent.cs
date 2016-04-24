using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Service;
using L.NENU.Domain;
using L.NENU.Manager;
using NHibernate.Criterion;

namespace L.NENU.Component
{
    public class BaseComponent<T, M> : IBaseService<T>
        where T : EntityBase, new()
        where M : ManagerBase<T>, new()
    {
        protected M manager = (M)typeof(M).GetConstructor(Type.EmptyTypes).Invoke(null);



        /// <summary>
        /// 根据条件获取对象
        /// </summary>
        /// <param name="queryConditions"></param>
        /// <returns></returns>
        public T Get(IList<ICriterion> queryConditions)
        {
            return manager.Get(queryConditions);
        }

        /// <summary>
        /// 根据对象ID 调用数据访问层 查询数据
        /// </summary>
        /// <param name="id">对象ID</param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            return manager.Get(id);
        }


        /// <summary>
        /// 通过实体 查询数据库中该实体的所有数据
        /// </summary>
        /// <returns></returns>
        public IList<T> GetAll()
        {
            return manager.GetAll();
        }

        /// <summary>
        /// 利用该数据的实体 删除数据库中对应的数据
        /// </summary>
        /// <param name="t">数据实体</param>
        public void Delete(T t)
        {
            manager.Delete(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        public virtual void Update(T t)
        {
            manager.Update(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        public virtual void Create(T t)
        {
            manager.Create(t);
        }

        #region  分页控件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryConditions"></param>
        /// <param name="orderList"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<T> GetPaged(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count)
        {
            return manager.GetPaged(queryConditions, orderList, pageIndex, pageSize, out count);
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
            return manager.GetPaged(queryConditions, pageIndex, pageSize, out count);
        }
        #endregion


        #region 存在判断


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            return manager.Exists(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryConditions"></param>
        /// <returns></returns>
        public bool Exists(IList<ICriterion> queryConditions)
        {
            return manager.Exists(queryConditions);
        }
        #endregion
    }
}
