using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Domain.Send;



namespace L.NENU.Core
{
    public interface IWeiXinXMLAssembly
    {

        string SendText(SText model);
        string SendImg(SImg model);
        string SendVoice(SVoice model);
        string SendVideo(SVideo model);
        string SendMusic(Smusic model);
        string SendNews(SNews model);

    }
}
