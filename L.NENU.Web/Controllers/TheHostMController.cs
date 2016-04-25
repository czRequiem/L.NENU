using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L.NENU.Domain;
using L.NENU.Core;
using L.NENU.Service;


namespace L.NENU.Web.Controllers
{
    public class TheHostMController : Controller
    {
        //
        // GET: /TheHostM/

        public ActionResult Index()
        {
            IList<TheHostInfo> list = Container.Instance.Resolve<ITheHostInfoService>().GetAll();

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        
        }

        [HttpPost]
        public ActionResult Create(TheHostInfo theHost)
        {
            Container.Instance.Resolve<ITheHostInfoService>().Create(theHost);

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            TheHostInfo theHostInfo = Container.Instance.Resolve<ITheHostInfoService>().Get(id);

            return View(theHostInfo);
        }

        [HttpPost]
        public ActionResult Edit(TheHostInfo theHostInfo)
        {
            Container.Instance.Resolve<ITheHostInfoService>().Update(theHostInfo);

            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {

            TheHostInfo theHostInfo = Container.Instance.Resolve<ITheHostInfoService>().Get(id);

            IList<ShowInfo> list = theHostInfo.Show;

            ViewBag.ShowInfoList = list;

            return View(theHostInfo);

        }

        public ActionResult Delete(int id)
        { 

            TheHostInfo theHostInfo = Container.Instance.Resolve<ITheHostInfoService>().Get(id);

            Container.Instance.Resolve<ITheHostInfoService>().Delete(theHostInfo);

            return RedirectToAction("Index");
        
        }

    }
}
