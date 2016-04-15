using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L.NENU.Core;
using L.NENU.Service;
using L.NENU.Component;
using System.Web.Security;
using L.NENU.Web.Models;

namespace L.NENU.Web.Apps
{
    public class WeiXinChatFlow
    {
        public void DoWeiXinType(Dictionary<string, string> Model)
        {
            //实例化一个微信消息 处理模型 实例
            IDoWeiXinChat doWeiXinChat = new DoWeiXinChat();
            if (Model.Count > 0)
            {
                string msgType = ReadWeiXinXml.ReadModel("MsgType", Model);

                switch (msgType)
                {
                    case "text":
                        doWeiXinChat.DoText(Model);//文本消息
                        break;
                    case "image":
                        doWeiXinChat.DoImg(Model);//图片
                        break;
                    case "voice": //声音
                        doWeiXinChat.DoVoice(Model);
                        break;
                    case "video"://视频
                        doWeiXinChat.DoVideo(Model);
                        break;
                    case "location"://地理位置
                        doWeiXinChat.DoLocation(Model);
                        break;
                    case "link"://链接
                        doWeiXinChat.DoLink(Model);
                        break;

                    case "event":
                        switch (ReadWeiXinXml.ReadModel("Event", Model))
                        {
                            case "subscribe":
                                if (ReadWeiXinXml.ReadModel("EventKey", Model).IndexOf("qrscene_") >= 0)
                                {
                                    doWeiXinChat.DoOnCode(Model);//带参数的二维码扫描关注
                                }
                                else
                                {
                                    doWeiXinChat.DoOn(Model);//普通关注
                                }
                                break;
                            case "unsubscribe":
                                doWeiXinChat.DoUnOn(Model);//取消关注
                                break;

                            case "SCAN":
                                doWeiXinChat.DoSubCode(Model);//已经关注的用户扫描带参数的二维码
                                break;
                            case "LOCATION"://用户上报地理位置
                                doWeiXinChat.DoSubLocation(Model);
                                break;
                            case "CLICK"://自定义菜单点击
                                doWeiXinChat.DoSubClick(Model);
                                break;
                            case "VIEW"://自定义菜单跳转事件
                                doWeiXinChat.DoSubView(Model);
                                break;
                        };
                        break;

                }
            }
        }

        /// <summary>
        /// 定义Token，与微信公共平台上的Token保持一致
        /// </summary>
        private const string Token = "StupidMe";

        /// <summary>
        /// 验证签名，检验是否是从微信服务器上发出的请求
        /// </summary>
        /// <param name="model">请求参数模型 Model</param>
        /// <returns>是否验证通过</returns>
        public bool CheckSignature(WeChatRequestModel model)
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

    }
}