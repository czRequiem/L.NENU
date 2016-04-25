using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace L.NENU.Domain
{
    [ActiveRecord("NoticeInfo")]
    public class NoticeInfo:EntityBase
    {
        /// <summary>
        /// 节目标题
        /// </summary>
        [Property(NotNull = true, Length = 50)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "不能超过50个字符")]
        [Display(Name = "公告标题")]
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Property]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Property(NotNull = true, Length = 1000)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(1000, ErrorMessage = "不能超过1000个字符")]
        [Display(Name = "内容")]
        public string Introduce { get; set; }


        /// <summary>
        /// 创建人  一对多关系 此处为一
        /// </summary>
        [BelongsTo("CreateUserID")]
        public TheHostInfo CreateUser { get; set; }


    }
}
