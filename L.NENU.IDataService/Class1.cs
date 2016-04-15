using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Send;

namespace L.NENU.IDataService
{
    public interface IWeiXin
    {

        string SendText(SText model);
        string SendImg(SImg model);
        string SendVoice(SVoice model);
        string SendVideo(SVideo model);
        string SendMusic(Smusic model);
        string SendNews(SNews model);

    }
}
