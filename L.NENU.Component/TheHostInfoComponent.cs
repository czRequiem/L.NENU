using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L.NENU.Service;
using NHibernate.Criterion;
using L.NENU.Domain;
using L.NENU.Manager;

namespace L.NENU.Component
{
    public class TheHostInfoComponent : BaseComponent<TheHostInfo, TheHostInfoManager>, ITheHostInfoService
    {
        public Domain.TheHostInfo Login(string userName, string password)
        {
            IList<ICriterion> criterionList = new List<ICriterion>();
            criterionList.Add(Expression.Eq("Account", userName));
            criterionList.Add(Expression.Eq("Password", password));

            TheHostInfo user = manager.Get(criterionList);  //!!!!

            return user;
        }
    }
}
