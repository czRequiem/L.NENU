using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Criterion;
using L.NENU.Domain;
using L.NENU.Core;
using L.NENU.Service;
using L.NENU.Web.Apps;

namespace L.NENU.Web.Controllers
{
    /// <summary>
    /// 后台节目信息控制器
    /// </summary>
    public class ShowInfoMController : Controller
    {
        #region   Index 视图
        /// <summary>
        /// 后台节目展示视图
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Index(int pageIndex = 1)
        {
            int count = 0;

            //初始化查询条件
            IList<ICriterion> listQuery = GetCondition();

            //排序方式为按照时间排序
            IList<Order> listOrder = new List<Order>() { new Order("CreateTime", false) };

            //调用后台分页查询方法
            IList<ShowInfo> list = Container.Instance.Resolve<IShowInfoService>().GetPaged(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);

            //实例化分页列表
            PageList<ShowInfo> pageList = list.ToPageList<ShowInfo>(pageIndex, PagerHelper.PageSize, count);
            return View(pageList);
        }

        /// <summary>
        /// 准备查询条件
        /// </summary>
        /// <returns>查询条件的集合</returns>
        private IList<ICriterion> GetCondition()
        {
            IList<ICriterion> queryConditions = new List<ICriterion>();
            return queryConditions;
        }
        #endregion


        #region  Create视图

        /// <summary>
        /// 创建节目 初始化页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            //查询出所有专辑
            IList<AlbumInfo> albumIfoList = Container.Instance.Resolve<IAlbumInfoService>().GetAll();

            //查询出所有主播
            IList<TheHostInfo> theHostList = Container.Instance.Resolve<ITheHostInfoService>().GetAll();

            //将集合保存在页面盒子中
            ViewBag.AlbumIfoList = albumIfoList;
            ViewBag.TheHostList = theHostList;

            return View();

        }

        [HttpPost]
        public ActionResult Create(ShowInfo showinfo, string albumID, string TheHostID)
        {
            showinfo.CreateTime = DateTime.Now;
            showinfo.ShowTime = "11";
            showinfo.Album = new AlbumInfo();
            showinfo.Album.ID = int.Parse(albumID);

            TheHostID = TheHostID.Replace(",,", ",");
            Container.Instance.Resolve<IShowInfoService>().Create(showinfo, TheHostID);

            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult Edit(int id)
        {
            ShowInfo showInfo = Container.Instance.Resolve<IShowInfoService>().Get(id);

            AlbumInfo albumInfo = new AlbumInfo();

            IList<AlbumInfo> albumIfoList = Container.Instance.Resolve<IAlbumInfoService>().GetAll();
            IList<TheHostInfo> theHostList = Container.Instance.Resolve<ITheHostInfoService>().GetAll();

            ViewBag.AlbumIfoList = albumIfoList;
            ViewBag.TheHostList = theHostList;

            return View(showInfo);

        }

        [HttpPost]
        public ActionResult Edit(ShowInfo showInfo, string albumID)
        {

            string c = albumID;
            return View();
        }

        
    }
}
