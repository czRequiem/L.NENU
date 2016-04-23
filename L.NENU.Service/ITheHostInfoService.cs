using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L.NENU.Service
{
    public interface ITheHostInfoService
    {
        Domain.TheHostInfo Login(string userName, string password);

        IList<Domain.TheHostInfo> GetAll();
    }
}
