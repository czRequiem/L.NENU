using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Core;
using L.NENU.Domain.Send;
using L.NENU.Service;
using L.NENU.Domain;

namespace L.NENU.Component
{
    public class DoWeiXinChat : IDoWeiXinChat
    {
        public IWeiXinXMLAssembly WeiXinXMLAssembly = new WeiXinXMLAssembly();

        //---------------------------------------------------------------------------------
        #region 接收普通消息
        //接收到文本消息  处理
        public void DoText(Dictionary<string, string> model)
        {

            SNews mN = new SNews();
            mN.FromUserName = ReadWeiXinXml.ReadModel("ToUserName", model);
            mN.ToUserName = ReadWeiXinXml.ReadModel("FromUserName", model);
            mN.CreateTime = long.Parse(ReadWeiXinXml.ReadModel("CreateTime", model));
            mN.MsgType = "news";



            ShowInfo showInfo = new ShowInfo ();
            showInfo.ShowTitle = ReadWeiXinXml.ReadModel("Content", model);

            List<ArticlesModel> listNews = new ShowInfoComponent().GetShowInfoBy(showInfo);

            if (listNews.Count > 5)
            {
                mN.ArticleCount = 5;
            }
            else
            {
                mN.ArticleCount = listNews.Count;
            }

            mN.Articles = listNews;
            ReadWeiXinXml.ResponseToEnd(WeiXinXMLAssembly.SendNews(mN));

            //SText mT = new SText();
            //string text = ReadWeiXinXml.ReadModel("Content", model).Trim();

            //mT.FromUserName = ReadWeiXinXml.ReadModel("ToUserName", model);
            //mT.ToUserName = ReadWeiXinXml.ReadModel("FromUserName", model);
            //mT.CreateTime = long.Parse(ReadWeiXinXml.ReadModel("CreateTime", model));

            //if (text == "?" || text == "？" || text == "帮助")
            //{
            //    mT.Content = ReadWeiXinXml.Menu();
            //    mT.MsgType = "text";
            //    ReadWeiXinXml.ResponseToEnd(WeiXinXMLAssembly.SendText(mT));
            //}
            //else
            //{
            //    SNews mN = new SNews();
            //    mN.FromUserName = ReadWeiXinXml.ReadModel("ToUserName", model);
            //    mN.ToUserName = ReadWeiXinXml.ReadModel("FromUserName", model);
            //    mN.CreateTime = long.Parse(ReadWeiXinXml.ReadModel("CreateTime", model));
            //    mN.MsgType = "news";

            //    //   以下为文章内容，  实际使用时，此处应该是一个跟数据库交互的方法，查询出文章
            //    //文章条数，  文章内容等   都应该由数据库查询出来的数据决定   这里测试，就模拟几条

            //    mN.ArticleCount = 5;
            //    List<ArticlesModel> listNews = new List<ArticlesModel>();
            //    for (int i = 0; i < 5; i++)
            //    {
            //        ArticlesModel ma = new ArticlesModel();
            //        ma.Title = "这是第" + (i + 1).ToString() + "篇文章";
            //        ma.Description = "-描述-" + i.ToString() + "-描述-";
            //        ma.PicUrl = "http://image6.tuku.cn/pic/wallpaper/dongwu/taipingniaogaoqingbizhi/s00" + (i + 1).ToString() + ".jpg";
            //        ma.Url = "http://www.cnblogs.com/mochen/";
            //        listNews.Add(ma);
            //    }
            //    mN.Articles = listNews;
            //    ReadWeiXinXml.ResponseToEnd(WeiXinXMLAssembly.SendNews(mN));
            //}
        }








        //接收到图片消息
        public void DoImg(Dictionary<string, string> model)
        {

        }

        //接收到语音消息
        public void DoVoice(Dictionary<string, string> model)
        {

        }
        //接收到视频消息
        public void DoVideo(Dictionary<string, string> model)
        {

        }


        //接收到地理位置信息
        public void DoLocation(Dictionary<string, string> model)
        {

        }
        //接收到链接消息
        public void DoLink(Dictionary<string, string> Model)
        {

        }
        #endregion
        //----------------------------------------------------------------------------

        //----------------------------------------------------------------------------
        #region 接收事件消息
        //普通关注
        public void DoOn(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadWeiXinXml.Menu();
            mT.FromUserName = ReadWeiXinXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadWeiXinXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadWeiXinXml.ReadModel("CreateTime", model));
            ReadWeiXinXml.ResponseToEnd(WeiXinXMLAssembly.SendText(mT));
        }
        //取消关注
        public void DoUnOn(Dictionary<string, string> model)
        {

        }
        //未关注用户扫描二维码参数
        public void DoOnCode(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadWeiXinXml.Menu();
            mT.FromUserName = ReadWeiXinXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadWeiXinXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadWeiXinXml.ReadModel("CreateTime", model));
            ReadWeiXinXml.ResponseToEnd(WeiXinXMLAssembly.SendText(mT));
        }
        //已经关注的用户扫描二维码参数
        public void DoSubCode(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadWeiXinXml.Menu();
            mT.FromUserName = ReadWeiXinXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadWeiXinXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadWeiXinXml.ReadModel("CreateTime", model));
            ReadWeiXinXml.ResponseToEnd(WeiXinXMLAssembly.SendText(mT));
        }





        //用户提交地理位置
        public void DoSubLocation(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }
        //自定义菜单点击
        public void DoSubClick(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }
        //自定义菜单跳转
        public void DoSubView(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }

        #endregion
        //-------------------------------------------------------------------------------------






    }
}
