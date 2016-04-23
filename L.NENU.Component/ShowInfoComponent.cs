using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Domain;
using L.NENU.Domain.Send;
using L.NENU.Manager;
using L.NENU.Service;

namespace L.NENU.Component
{
    public class ShowInfoComponent  :BaseComponent<ShowInfo, ShowInfoManager> ,IShowInfoService
    {

        //实例化 数据库访问类
        ShowInfoManager m = new ShowInfoManager();

        #region 封存代码
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showInfo"></param>
        /// <returns></returns>
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

        #endregion

        /// <summary>
        /// 返回showinfo表中最新的数据前五条
        /// </summary>
        /// <returns></returns>
        public IList<ShowInfo> GetShowInfoByTop5()
        {
            ShowInfoManager m = new ShowInfoManager();
            IList<ShowInfo> list = m.GetShowInfoByTop5();
            return list;
        
        }


        /// <summary>
        /// 创建一条节目信息
        /// </summary>
        /// <param name="showinfo">节目信息实体</param>
        /// <param name="TheHostID">主播ID字符串集合</param>
        public void Create(ShowInfo showinfo, string TheHostID)
        {
            //调用数据库访问层 在节目信息表中创建一条节目信息
            manager.Create(showinfo);

            
            //调用封装方法 创建该节目的主播
            CreateTheHostList(showinfo.ID, TheHostID);

        }

        public void CreateTheHostList(int showInfoID, string TheHostIDList)
        {
            //在数据库中查询刚才创建的数据
            ShowInfo show = manager.Get(showInfoID);

            //判断该节目信息的主播集合是否存在
            if (show.TheHostInfo.Count == 0)
            {
                show.TheHostInfo = new List<TheHostInfo>();
            }
            //清空所有清空所有主播记录
            show.TheHostInfo.Clear();
            if (!string.IsNullOrEmpty(TheHostIDList))
            {
                string[] ids = TheHostIDList.Split(new char[] { ',', '_', '|' });
                foreach (string tempID in ids)
                {
                    if (string.IsNullOrEmpty(tempID))
                    {
                        continue;
                    }

                    TheHostInfo t = new TheHostInfoManager().Get(int.Parse(tempID));
                    show.TheHostInfo.Add(t);
                }
            }

            manager.Update(show);
        }


        public void Update(ShowInfo showInfo, string TheHostID)
        {
            manager.Update(showInfo);
            CreateTheHostList(showInfo.ID,TheHostID);
        }
    }
}
