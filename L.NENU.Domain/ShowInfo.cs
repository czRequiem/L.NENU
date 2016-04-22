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
    [ActiveRecord("ShowInfo")]
    public class ShowInfo : EntityBase
    {
        #region  非关系映射属性

        /// <summary>
        /// 节目编号
        /// </summary>
        [Property(NotNull = true)]
        [Required]
        [Display(Name = "Counter")]
        public int Number { get; set; }

        /// <summary>
        /// 节目标题
        /// </summary>
        [Property(NotNull = true, Length = 100)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(100, ErrorMessage = "不能超过100个字符")]
        [Display(Name = "节目标题")]
        public string ShowTitle { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Property]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 节目时间
        /// </summary>
        [Property(NotNull = true, Length = 20)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(20, ErrorMessage = "不能超过20个字符")]
        [Display(Name = "节目时长")]
        public string ShowTime { get; set; }

        /// <summary>
        /// 节目介绍
        /// </summary>
        [Property(NotNull = true, Length = 200)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(200, ErrorMessage = "不能超过200个字符")]
        [Display(Name = "节目介绍")]
        public string intro { get; set; }

        /// <summary>
        /// 节目播放地址
        /// </summary>
        [Property(NotNull = true, Length = 200)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(200, ErrorMessage = "不能超过200个字符")]
        [Display(Name = "节目播放地址")]
        public string HtmlUrl { get; set; }

        /// <summary>
        /// 节目图片地址
        /// </summary>
        [Property(NotNull = true, Length = 200)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(200, ErrorMessage = "不能超过200个字符")]
        [Display(Name = "节目图片地址")]
        public string ImgUrl { get; set; }

        #endregion

        #region  外键关系

        /// <summary>
        /// 节目集合 一对多关系 此处为一 albumInfo中为多
        /// </summary>
        [BelongsTo("AlbumID")]
        public AlbumInfo Album { get; set; }

        /// <summary>
        /// 创建人  多对多关系
        /// </summary>
        [HasAndBelongsToMany(typeof(TheHostInfo),
            Table = "Show_TheHost_Info",
            ColumnKey = "SystemFunctionID",
            ColumnRef = "TheHostInfoID",
            Cascade = ManyRelationCascadeEnum.All,
            Inverse = false,
            Lazy = false)]
        public IList<TheHostInfo> TheHostInfo { get; set; }

        #endregion

    }
}
