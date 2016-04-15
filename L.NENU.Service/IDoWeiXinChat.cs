using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L.NENU.Service
{
    /// <summary>
    /// 微信消息判断 处理 模型 接口 
    /// </summary>
    public interface IDoWeiXinChat
    {
        #region 接收普通消息
        //接收文本消息
        void DoText(Dictionary<string, string> model);

        //接收到图片消息
        void DoImg(Dictionary<string, string> model);

        //接收到语音消息
        void DoVoice(Dictionary<string, string> model);

        //接收到视频消息
        void DoVideo(Dictionary<string, string> model);

        //接收到地理位置信息
        void DoLocation(Dictionary<string, string> model);

        //接收到链接消息
        void DoLink(Dictionary<string, string> Model);
        #endregion

        #region 接收事件消息
        //普通关注
        void DoOn(Dictionary<string, string> Model);

        //取消关注
        void DoUnOn(Dictionary<string, string> Model);

        //未关注用户扫描二维码参数
        void DoOnCode(Dictionary<string, string> Model);

        //已经关注的用户扫描二维码参数
        void DoSubCode(Dictionary<string, string> Model);

        //用户提交地理位置
        void DoSubLocation(Dictionary<string, string> Model);

        //自定义菜单点击
        void DoSubClick(Dictionary<string, string> Model);

        //自定义菜单跳转
        void DoSubView(Dictionary<string, string> Model);
        #endregion
    }
}