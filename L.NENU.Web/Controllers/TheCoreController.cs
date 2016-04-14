using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using L.NENU.Web.Models;

namespace L.NENU.Web.Controllers
{
    public class TheCoreController : Controller
    {

        /// <summary>
        /// 定义Token，与微信公共平台上的Token保持一致
        /// </summary>
        private const string Token = "StupidMe";

        /// <summary>
        /// 验证签名，检验是否是从微信服务器上发出的请求
        /// </summary>
        /// <param name="model">请求参数模型 Model</param>
        /// <returns>是否验证通过</returns>
        private bool CheckSignature(WeChatRequestModel model)
        {
            string signature, timestamp, nonce, tempStr;
            //获取请求来的参数
            signature = model.signature;
            timestamp = model.timestamp;
            nonce = model.nonce;
            //创建数组，将 Token, timestamp, nonce 三个参数加入数组
            string[] array = { Token, timestamp, nonce };
            //进行排序
            Array.Sort(array);
            //拼接为一个字符串
            tempStr = String.Join("", array);
            //对字符串进行 SHA1加密
            tempStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tempStr, "SHA1").ToLower();
            //判断signature 是否正确
            if (tempStr.Equals(signature))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Valid(WeChatRequestModel model)
        {
            //获取请求来的 echostr 参数
            string echoStr = model.echostr;
            //通过验证
            if (CheckSignature(model))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    //将随机生成的 echostr 参数 原样输出
                    Response.Write(echoStr);
                    //截止输出流
                    Response.End();
                }
            }
        }


        public ActionResult Test()
        {
            return View();
        }
    }
}



