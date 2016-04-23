using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L.NENU.Web.Apps;
using L.NENU.Core;
using L.NENU.Service;

namespace L.NENU.Web.Controllers
{
    public class TheHostController : Controller
    {
        //
        // GET: /TheHost/

        #region  login视图的实现
        /// <summary>
        /// 后台登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录动作  当页面 进行提交的时候进行
        /// </summary>
        /// <param name="userName">登录用户名</param>
        /// <param name="password">登陆密码</param>
        /// <returns>对请求的处理结果</returns>
        [HttpPost]  //表示该方法仅用于处理Post请求  即页面表单提交的post请求
        public ActionResult Login(string userName, string password)
        {
            //password = AppHelper.EncodeMd5(password);  //加密传入的密码

            //进行数据库查询 
            Domain.TheHostInfo loginUser = Container.Instance.Resolve<ITheHostInfoService>().Login(userName, password);

            if (loginUser != null)  //判断是否登登录成功
            {
                AppHelper.LoginedUser = loginUser;  //在静态属性中保存用户登录信息
                return RedirectToAction("Index", "ShowInfoM");  //RedirectToAction使用操作名称和控制器名称重定向到指定的操作
            }
            else
            {
                return View();  //返回到原视图
            }

        }
        #endregion

    }
}
