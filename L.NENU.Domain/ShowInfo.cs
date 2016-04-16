using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L.NENU.Domain
{
    public class ShowInfo
    {
        public int ID { get; set; }

        public string ShowTitle { get; set; }

        public DateTime CreateTime { get; set; }

        public string ShowTime { get; set; }

        public string intro { get; set; }

        public string HtmlUrl {get;set;}

        public string ImgUrl { get; set; }

    }
}
