using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L.NENU.Service
{
    public interface IAlbumInfoService
    {
        IList<Domain.AlbumInfo> GetAll();

        void Create(Domain.AlbumInfo albumInfo);

        Domain.AlbumInfo Get(int id);

        void Update(Domain.AlbumInfo albumInfo);

        void Delete(Domain.AlbumInfo albumInfo);
    }
}
