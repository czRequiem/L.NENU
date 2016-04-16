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
