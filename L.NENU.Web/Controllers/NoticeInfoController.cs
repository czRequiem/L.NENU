using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L.NENU.Core;
using L.NENU.Domain;
using L.NENU.Service;
using L.NENU.Web.Apps;

namespace L.NENU.Web.Controllers
{
    public class NoticeInfoController : Controller
    {
        //
        // GET: /NoticeInfo/

        public ActionResult Index()
        {
            IList<NoticeInfo> list = Container.Instance.Resolve<INoticeInfoService>().GetAll();

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NoticeInfo noticeInfo)
        {
            noticeInfo.CreateTime = DateTime.Now;
            noticeInfo.CreateUser = AppHelper.LoginedUser;

            Container.Instance.Resolve<INoticeInfoService>().Create(noticeInfo);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            NoticeInfo noticeInfo = Container.Instance.Resolve<INoticeInfoService>().Get(id);

            return View(noticeInfo);
        }


        public ActionResult Edit(NoticeInfo noticeInfo)
        {
            Container.Instance.Resolve<INoticeInfoService>().Update(noticeInfo);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            NoticeInfo noticeInfo = Container.Instance.Resolve<INoticeInfoService>().Get(id);

            return View(noticeInfo);
        }

    }
}
