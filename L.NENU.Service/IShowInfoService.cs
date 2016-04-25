using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Domain;

namespace L.NENU.Service
{
    public interface IShowInfoService
    {

        /// <summary>
        /// 返回节目信息前五条数据供给首页显示
        /// </summary>
        /// <returns></returns>
        IList<ShowInfo> GetShowInfoByTop5();


        /// <summary>
        /// 分页查询集合
        /// </summary>
        /// <param name="listQuery"></param>
        /// <param name="listOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="p"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IList<ShowInfo> GetPaged(IList<NHibernate.Criterion.ICriterion> listQuery, IList<NHibernate.Criterion.Order> listOrder, int pageIndex, int p, out int count);

        /// <summary>
        /// 根据ID返回节目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ShowInfo Get(int id);

        /// <summary>
        /// 创建一条节目信息 
        /// </summary>
        /// <param name="showinfo">节目信息实体</param>
        /// <param name="TheHostID">创建人ID字符串集合</param>
        void Create(ShowInfo showinfo, string TheHostID);

        void Update(ShowInfo showInfo, string TheHostID);


        void Delete(ShowInfo showInfo);


        /// <summary>
        /// 指定开始地点  指定返回行数  返回指定行数
        /// </summary>
        /// <param name="start">开始行不包括本行</param>
        /// <param name="size">返回行数</param>
        /// <param name="listOrder">排序条件</param>
        /// <param name="criterionList">查询条件</param>
        /// <returns></returns>
        IList<ShowInfo> SlicedFindAll(int start, int size, IList<NHibernate.Criterion.Order> listOrder, IList<NHibernate.Criterion.ICriterion> criterionList);
    }
}
