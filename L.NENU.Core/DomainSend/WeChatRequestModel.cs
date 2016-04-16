using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L.NENU.Core.DomainSend
{
    public class WeChatRequestModel
    {
        public string signature { get; set; }
        public string timestamp { get; set; }
        public string nonce { get; set; }
        public string echostr { get; set; }
    }

}