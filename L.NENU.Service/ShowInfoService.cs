using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Domain;

namespace L.NENU.Service
{
    public interface ShowInfoService
    {
        IList<ShowInfo> GetShowInfoByTop5();
    }
}
