using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Domain;
using L.NENU.Service;
using L.NENU.Manager;

namespace L.NENU.Component
{
    public class NoticeInfoComponent : BaseComponent<NoticeInfo, NoticeInfoManager>, INoticeInfoService
    {
    }
}
