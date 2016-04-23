using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L.NENU.Service;
using L.NENU.Component;
using L.NENU.Domain;

namespace L.NUNE.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            IShowInfoService ss = new ShowInfoComponent();
            IList<ShowInfo> list = ss.GetShowInfoByTop5();
            return View(list);
        }

    }
}
