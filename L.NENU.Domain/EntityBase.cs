using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace L.NENU.Domain
{
    public class EntityBase
    {
        [PrimaryKey(PrimaryKeyType.Identity)]//采用Castle.Net提供的高低位算法生成ID
        [Display(AutoGenerateField = false)]//在生成MVC3.0视图时不生成本属性
        public virtual int ID { get; set; }//主键
    }
}
