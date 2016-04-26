using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L.NENU.Service;
using L.NENU.Component;
using L.NENU.Domain;
using L.NENU.Core;
using NHibernate.Criterion;

namespace L.NUNE.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        /// <summary>
        /// 首页展示 2016年4月25日 18:23:29 林
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region  废弃代码
            //IShowInfoService ss = new ShowInfoComponent();
            //IList<ShowInfo> list = ss.GetShowInfoByTop5();
            //return View(list);

            #endregion

            //排序方式为按照时间排序
            IList<Order> listOrder = new List<Order>() { new Order("CreateTime", false) };
            IList<ICriterion> criterionList = GetCondition();  //准备查询条件
            int size =5;  //返回的条目数量
            int start = 0;  //从第几行开始  不包括本行
            IList<ShowInfo> listz = Container.Instance.Resolve<IShowInfoService>().SlicedFindAll( start,size, listOrder, criterionList);
            return View(listz);

        }

        /// <summary>
        /// 准备查询条件 2016-4-23 21:48:22 林
        /// </summary>
        /// <returns>查询条件的集合</returns>
        private IList<ICriterion> GetCondition()
        {
            IList<ICriterion> queryConditions = new List<ICriterion>();
            return queryConditions;
        }


        public ActionResult Introduce1314()
        {
            return View();
        }
    }
}
