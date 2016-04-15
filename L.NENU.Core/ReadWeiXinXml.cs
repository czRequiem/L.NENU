using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace L.NENU.Core
{

    /// <summary>
    /// 微信XML数据的处理
    /// </summary>
    public static class ReadWeiXinXml
    {
        /// <summary>
        /// 把接收到的XML转为字典  2016年4月15日 10:27:19 林
        /// </summary>
        /// <param name="xmlStr">微信XML消息</param>
        /// <returns>一个字典 多行  每一行 包含 名称 和内容符号</returns>
        public static Dictionary<string, string> XmlModel(string xmlStr)
        {
            //实例化一个XML文档
            XmlDocument doc = new XmlDocument();

            //将微信消息字符 加载到XML文档
            doc.LoadXml(xmlStr);

            //实例化一个字典
            Dictionary<string, string> mo = new Dictionary<string, string>();

            //转换XML文档 节点串
            var data = doc.DocumentElement.ChildNodes;
            //.SelectNodes("xml");

            //循环添加字典列
            for (int i = 0; i < data.Count; i++)
            {
                //获取指定序列的节点名  以及节点所包含的字符
                mo.Add(data.Item(i).LocalName, data.Item(i).InnerText);
            }

            //返回这个字典
            return mo;
        }



        ////从字典中读取指定的值
        public static string ReadModel(string key, Dictionary<string, string> model)
        {
            string str = "";
            model.TryGetValue(key, out str);
            if (str == null)
                str = "";

            return str;
        }

        //输出字符串并结束当前页面进程 MVC必须加return
        public static void ResponseToEnd(string str)
        {
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
            return;
        }

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


    }
}
