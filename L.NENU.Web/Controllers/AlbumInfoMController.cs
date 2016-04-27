using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L.NENU.Core;
using System.Collections;
using L.NENU.Domain;
using L.NENU.Service;

namespace L.NENU.Web.Controllers
{
    /// <summary>
    /// 后台专辑信息控制器
    /// </summary>
    public class AlbumInfoMController : Controller
    {
        //
        // GET: /AlbumInfoM/

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IList<AlbumInfo> list = Container.Instance.Resolve<IAlbumInfoService>().GetAll();
            return View(list);
        }

        public ActionResult Create()
        {
            int x = 1;
            return View();
        }

        [HttpPost]
        public ActionResult Create(AlbumInfo albumInfo)
        {
            albumInfo.CreateTime = DateTime.Now;
            Container.Instance.Resolve<IAlbumInfoService>().Create(albumInfo);

            return  RedirectToAction("Index");

        }

        public ActionResult Edit( int id)
        {

            AlbumInfo albumInfo = Container.Instance.Resolve<IAlbumInfoService>().Get(id);
            return View(albumInfo);
        }

        [HttpPost]
        public ActionResult Edit(AlbumInfo albumInfo)
        {

            albumInfo.CreateTime = DateTime.Now;
            Container.Instance.Resolve<IAlbumInfoService>().Update(albumInfo);

            return RedirectToAction("Index");
            
        }


        public ActionResult Details(int id)
        {
            AlbumInfo albumInfo = Container.Instance.Resolve<IAlbumInfoService>().Get(id);

            IList<ShowInfo> list = albumInfo.Show;

            ViewBag.ShowInfoList = list;

            return View(albumInfo);
        }

        public ActionResult Delete(int id)
        { 
            AlbumInfo albumInfo = Container.Instance.Resolve<IAlbumInfoService>().Get(id);

            Container.Instance.Resolve<IAlbumInfoService>().Delete(albumInfo);

            return RedirectToAction("Index");
        }
    }
}
