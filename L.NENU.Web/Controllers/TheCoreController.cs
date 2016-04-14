using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using L.NENU.Web.Models;
using System.IO;
using System.Xml;
using L.NENU.Domain;
using System.Text;

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


        /// <summary>
        /// 接收微信发送的XML消息并且解析
        /// </summary>
        [HttpPost]
        private void Valid()
        {
            string xmlFromWeChat = new StreamReader(Request.InputStream).ReadToEnd();

            if (!string.IsNullOrEmpty(xmlFromWeChat))
            {
                //封装请求类
                XmlDocument requestDocXml = new XmlDocument();
                requestDocXml.LoadXml(xmlFromWeChat);
                XmlElement rootElement = requestDocXml.DocumentElement;
                WxXmlModel WxXmlModel = new WxXmlModel();
                WxXmlModel.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                WxXmlModel.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                WxXmlModel.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                WxXmlModel.MsgType = rootElement.SelectSingleNode("MsgType").InnerText;
                switch (WxXmlModel.MsgType)
                {
                    case "text"://文本
                        WxXmlModel.Content = rootElement.SelectSingleNode("Content").InnerText;
                        break;
                    case "image"://图片
                        WxXmlModel.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                        break;
                    case "event"://事件
                        WxXmlModel.Event = rootElement.SelectSingleNode("Event").InnerText;
                        if (WxXmlModel.Event == "subscribe")//关注类型
                        {
                            WxXmlModel.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                        }
                        break;
                    default:
                        break;
                }
                string resxml = getText(WxXmlModel);
                //return WxXmlModel;
                Response.Write(resxml);
            }
        }


        private string textCase(WxXmlModel xmlMsg)
        {
            int nowtime = ConvertDateTimeInt(DateTime.Now);
            string msg = "";
            msg = getText(xmlMsg);
            string resxml = "<xml><ToUserName><![CDATA[" + xmlMsg.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + xmlMsg.ToUserName + "]]></FromUserName><CreateTime>" + nowtime + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + msg + "]]></Content><FuncFlag>0</FuncFlag></xml>";
            return resxml;
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        private int converDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        private string getText(WxXmlModel xmlMsg)
        {
            string con = xmlMsg.Content.Trim();

            System.Text.StringBuilder retsb = new StringBuilder(200);
            retsb.Append("这里放你的业务逻辑");
            retsb.Append("接收到的消息：" + xmlMsg.Content);
            retsb.Append("用户的OPEANID：" + xmlMsg.FromUserName);

            return retsb.ToString();
        }


    }
}