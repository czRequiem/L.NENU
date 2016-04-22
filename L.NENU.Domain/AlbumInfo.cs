using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;
namespace L.NENU.Domain
{
    /// <summary>
    /// 节目信息类
    /// </summary>
    [ActiveRecord("AlbumInfo")]
    public class AlbumInfo : EntityBase
    {
        /// <summary>
        /// 节目集名字
        /// </summary>
        [Property(NotNull = true, Length = 50)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "不能超过50个字符")]
        [Display(Name = "节目集名字")]
        public string Name { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [Property(NotNull = true, Length = 200)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(200, ErrorMessage = "不能超过200个字符")]
        [Display(Name = "介绍")]
        public string Introduce { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Property]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 节目集合  一对多关系 此处为多
        /// </summary>
        [HasMany(typeof(ShowInfo), ColumnKey = "AlbumID", Cascade = ManyRelationCascadeEnum.None, Inverse = false, Lazy = false)]
        public IList<ShowInfo> Show { get; set; }


    }
}
