using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L.NENU.Domain
{

    public class RoleInfo:EntityBase
    {
        public string Name { get; set; }


        ///// <summary>
        ///// 节目集合 一对一关系
        ///// </summary>
        //[HasAndBelongsToMany(typeof(RoleInfo),
        //    Table = "TheHostInfoI_RoleInfo_Info",
        //    ColumnKey = "TheHostInfoID",
        //    ColumnRef = "RoleInfoID",
        //    Cascade = ManyRelationCascadeEnum.All,
        //    Inverse = false,
        //    Lazy = false)]
        public RoleInfo Role { get; set; }
    }
}
