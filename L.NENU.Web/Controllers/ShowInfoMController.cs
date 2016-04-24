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
        /// 后台节目展示视图  2016年4月23日 21:48:12  林
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
        /// 准备查询条件 2016-4-23 21:48:22 林
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
        /// 创建节目 初始化页面 2016年4月23日 21:47:55 林
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


        /// <summary>
        /// 创建视图保响应 2016年4月23日 21:47:48 林
        /// </summary>
        /// <param name="showinfo">节目信息实体</param>
        /// <param name="albumID">专辑ID</param>
        /// <param name="TheHostID">主播ID集合</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(ShowInfo showinfo, string albumID, string TheHostID)
        {
            //准备初始化数据
            showinfo.CreateTime = DateTime.Now;
            showinfo.ShowTime = "10:10";
            showinfo.Album = new AlbumInfo();
            showinfo.Album.ID = int.Parse(albumID);

            //重组页面的字符串
            TheHostID = TheHostID.Replace(",,", ",");

            Container.Instance.Resolve<IShowInfoService>().Create(showinfo, TheHostID);

            return RedirectToAction("Index");
        }
        #endregion


        #region  Edit视图响应方法
        /// <summary>
        /// 修改页面 数据准备 2016年4月23日 22:09:16 林
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            //从数据库中查询出 节目信息
            ShowInfo showInfo = Container.Instance.Resolve<IShowInfoService>().Get(id);

            //查询专辑 主播 所有数据供选择
            IList<AlbumInfo> albumIfoList = Container.Instance.Resolve<IAlbumInfoService>().GetAll();
            IList<TheHostInfo> theHostList = Container.Instance.Resolve<ITheHostInfoService>().GetAll();

            //保存在页面盒子中
            ViewBag.AlbumIfoList = albumIfoList;
            ViewBag.TheHostList = theHostList;

            //由于视图引擎语言限制 只能预先准备字符串
            string tmp = "";

            foreach (var tmplist in showInfo.TheHostInfo)
            {
                tmp = tmplist.ID + ",";
            }
            ViewBag.TheHostID = tmp;

            return View(showInfo);

        }

        /// <summary>
        /// 修改页面 返回数据 进行修改 2016年4月23日 22:07:40 林
        /// </summary>
        /// <param name="showInfo"></param>
        /// <param name="albumID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ShowInfo showInfo, string albumID, string TheHostID)
        {
            //查询数据库 填充可能会被失误更改的 不能更改的值
            ShowInfo showInfoLog = Container.Instance.Resolve<IShowInfoService>().Get(showInfo.ID);
            showInfo.ShowTime = showInfoLog.ShowTime;
            showInfo.CreateTime = showInfoLog.CreateTime;
            showInfo.TheHostInfo = showInfoLog.TheHostInfo;
            //重组页面的字符串
            TheHostID = TheHostID.Replace(",,", ",");
            showInfo.Album = new AlbumInfo();
            showInfo.Album.ID = int.Parse(albumID);

            Container.Instance.Resolve<IShowInfoService>().Update(showInfo, TheHostID);

            return RedirectToAction("Index");
        }

        #endregion


        #region  delete反应
        public ActionResult Delete(int id)
        {
            ShowInfo showInfo = Container.Instance.Resolve<IShowInfoService>().Get(id);
            Container.Instance.Resolve<IShowInfoService>().Delete(showInfo);

            return RedirectToAction("index");
        }

        #endregion
    }
}
