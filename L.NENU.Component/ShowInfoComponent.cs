using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Domain;
using L.NENU.Domain.Send;
using L.NENU.Manager;

namespace L.NENU.Component
{
    public class ShowInfoComponent
    {


        //输出字符串并结束当前页面进程 MVC必须加return
        public static string Menu()
        {
            string Content = "";
            Content += "欢迎使用XXXX/微笑\n\n";
            Content += "输入以下序号开始获取最新信息：\n";
            Content += "1,新闻30分\n";
            Content += "2,电影预告\n";
            Content += "3,今日说法\n";
            Content += "4,焦点访谈\n";
            Content += "5,新闻联播\n";

            Content += "输入?或帮助 可以显示此菜单";
            return Content;
        }


        ShowInfoManager m = new ShowInfoManager();

        public List<ArticlesModel> GetShowInfoBy(ShowInfo showInfo)
        {
            showInfo.ShowTitle = "%" + showInfo.ShowTitle + "%";
            List<ArticlesModel> listNews = new List<ArticlesModel>();

            IList<ShowInfo> list = m.GetShowInfoBy(showInfo);

            foreach (var s in list)
            {
                ArticlesModel ma = new ArticlesModel();
                ma.Title = s.ShowTitle;
                ma.Description = s.intro;
                ma.PicUrl = s.ImgUrl;
                ma.Url = s.HtmlUrl;
                listNews.Add(ma);

            }

            return listNews;
        }
    }
}
