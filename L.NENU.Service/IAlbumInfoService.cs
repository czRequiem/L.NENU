using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L.NENU.Service
{
    public interface IAlbumInfoService
    {
        IList<Domain.AlbumInfo> GetAll();
    }
}
