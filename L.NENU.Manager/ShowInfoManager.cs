using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Domain;
using System.Data.SqlClient;
using System.Data;

namespace L.NENU.Manager
{
    public class ShowInfoManager:ManagerBase<ShowInfo>
    {
        public IList<ShowInfo> GetShowInfoBy(ShowInfo showInfo)
        {
            //准备存储过程名字
            string sql = "sp_ShowInfo_ShowInfoSelete_By";

            if (showInfo.ShowTitle == null)
            {
                showInfo.ShowTitle = "";
            }
            if (showInfo.intro == null)
            {
                showInfo.intro = "";
            }

            //准备参数
            SqlParameter[] spar = new SqlParameter[3];
            spar[0] = new SqlParameter("@ID", showInfo.ID);
            spar[1] = new SqlParameter("@ShowTitle", showInfo.ShowTitle);
            spar[2] = new SqlParameter("@Intro", showInfo.intro);

            IList<ShowInfo> list = new List<ShowInfo>();

            SqlDataReader reader = SqlHelper.GetDataReader(CommandType.StoredProcedure, sql, spar);

            while (reader.Read())
            {
                ShowInfo s = new ShowInfo();
                s.ID = int.Parse(reader["ID"].ToString());
                s.ShowTitle = reader["ShowTitle"].ToString();
                s.intro = reader["intro"].ToString();
                s.CreateTime = Convert.ToDateTime( reader["CreateTime"].ToString());
                s.ShowTime = reader["ShowTime"].ToString();
                s.HtmlUrl = reader["HtmlUrl"].ToString();
                s.ImgUrl = reader["ImgUrl"].ToString();

                list.Add(s);
            }

            reader.Close();
            reader.Dispose();

            return list;
        }


        /// <summary>
        /// 返回showinfo表中最新的数据前五条
        /// </summary>
        /// <returns></returns>
        public IList<ShowInfo> GetShowInfoByTop5()
        {

            
            //准备存储过程名字
            string sql = "sp_ShowInfo_TheHost_BYTop5";

            IList<ShowInfo> list = new List<ShowInfo>();

            SqlDataReader reader = SqlHelper.GetDataReader(CommandType.StoredProcedure, sql);

            while (reader.Read())
            {
                ShowInfo s = new ShowInfo();
                s.ID = int.Parse(reader["ID"].ToString());
                s.ShowTitle = reader["ShowTitle"].ToString();
                s.intro = reader["intro"].ToString();
                s.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                s.ShowTime = reader["ShowTime"].ToString();
                s.HtmlUrl = reader["HtmlUrl"].ToString();
                s.ImgUrl = reader["ImgUrl"].ToString();

                list.Add(s);
            }

            reader.Close();
            reader.Dispose();

            return list;
        }
    }
}
