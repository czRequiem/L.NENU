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
using L.NENU.Service;
using L.NENU.Core;
using L.NENU.Component;
using L.NENU.Web.Apps;
namespace L.NENU.Web.Controllers
{
    public class TheCoreController : Controller
    {

        /// <summary>
        /// 微信传过来的消息处理方法
        /// </summary>
        public void Index()
        {
            //实例化一个字典 多行 包含两个字符串  第一个表示名称 第二个存所包含的字符
            Dictionary<string, string> Model = new Dictionary<string, string>();

            //定义一个空的字符串 用于存放微信传过来的消息
            string xmlData = string.Empty;

            //检查传入的访问 是否附带poot
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                //将Post  消息 序列化  并自动释放内存
                using (Stream stream = Request.InputStream)
                {
                    Byte[] byteData = new Byte[stream.Length];
                    stream.Read(byteData, 0, (Int32)stream.Length);
                    xmlData = Encoding.UTF8.GetString(byteData);
                }

                //判断是否转换成功 存在字符
                if ((xmlData + "").Length > 0)//!string.IsNullOrEmpty(xmlData)
                {
                    try
                    {
                        //传递给核心层 格式化数据  传回数据字典
                        Model = ReadWeiXinXml.XmlModel(xmlData);

                        new WeiXinChatFlow().DoWeiXinType(Model);
                    }
                    catch
                    {
                        //未能正确处理 给微信服务器回复默认值
                        DefaultResult();
                    }
                }
                else
                {
                    //未能正确处理 给微信服务器回复默认值
                    DefaultResult();
                }

            }
            else //如果并非Post 请求
            {
                //get
                WeChatRequestModel weixinR = new WeChatRequestModel();
                weixinR.signature = Request.QueryString["signature"];
                weixinR.timestamp = Request.QueryString["timestamp"];
                weixinR.nonce = Request.QueryString["nonce"];
                weixinR.echostr = Request.QueryString["echostr"];
                //通过验证
                if (new WeiXinChatFlow().CheckSignature(weixinR))
                {
                    if (!string.IsNullOrEmpty(weixinR.echostr))
                    {
                        //这里直接返回echostr参数接入成功;
                        ReadWeiXinXml.ResponseToEnd(weixinR.echostr);
                    }
                }
            }

        }

        /// <summary>
        /// 当无法处理请求的时候 返回 默认值
        /// </summary>
        public void DefaultResult()
        {
            ReadWeiXinXml.ResponseToEnd("对不起 你所问的问题我现在无法回答 请联系开发者 林 微信号 DothL ");
        }
    }
}
