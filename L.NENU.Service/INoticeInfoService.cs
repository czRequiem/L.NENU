using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L.NENU.Service
{
    public interface INoticeInfoService
    {
        IList<Domain.NoticeInfo> GetAll();

        void Create(Domain.NoticeInfo noticeInfo);

        Domain.NoticeInfo Get(int id);

        void Update(Domain.NoticeInfo noticeInfo);
    }
}
