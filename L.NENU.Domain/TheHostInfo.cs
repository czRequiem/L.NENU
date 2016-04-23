using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace L.NENU.Domain
{

    /// <summary>
    /// 主播信息类
    /// </summary>
    [ActiveRecord("TheHostInfo")]
    public class TheHostInfo : EntityBase
    {

        #region  非关系映射属性

        /// <summary>
        /// 登录账号
        /// </summary>
        [Property(NotNull = true, Length = 30)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(30, ErrorMessage = "不能超过30个字符")]
        [Display(Name = "登录账号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Property(NotNull = true, Length = 16)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(16, ErrorMessage = "不能超过16个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 主播名
        /// </summary>
        [Property(NotNull = true, Length = 20)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(20, ErrorMessage = "不能超过20个字符")]
        [Display(Name = "主播名")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Property(NotNull = true, Length = 10)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(10, ErrorMessage = "不能超过10个字符")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 入社时间
        /// </summary>
        [Property]
        [Display(Name = "入社时间")]
        public DateTime JoinTime { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Property]
        [Display(Name = "生日")]
        public DateTime BirthTime { get; set; }

        /// <summary>
        /// 网络照片地址
        /// </summary>
        [Property(Length = 200)]
        [StringLength(200, ErrorMessage = "不能超过200个字符")]
        [Display(Name = "网络照片地址")]
        public string PhptoUrl { get; set; }

        /// <summary>
        /// 个人介绍
        /// </summary>
        [Property(NotNull = true, Length = 1000)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(1000, ErrorMessage = "不能超过1000个字符")]
        [Display(Name = "个人介绍")]
        public string Introduce { get; set; }
        #endregion


        #region  外键关系

        ///// <summary>
        ////角色集合 一对一关系
        ///// </summary>
        //[HasAndBelongsToMany(typeof(RoleInfo),
        //    Table = "TheHostInfoI_RoleInfo_Info",
        //    ColumnKey = "TheHostInfoID",
        //    ColumnRef = "RoleInfoID",
        //    Cascade = ManyRelationCascadeEnum.All,
        //    Inverse = false,
        //    Lazy = false)]
        //public RoleInfo Role { get; set; }


        /// <summary>
        /// 此人发布的公告 一对多关系 此处为多
        /// </summary>
        [HasMany(typeof(NoticeInfo), ColumnKey = "CreateUserID", Cascade = ManyRelationCascadeEnum.All, Inverse = false, Lazy = false)]
        public IList<NoticeInfo> Notice { get; set; }

        /// <summary>
        /// 此人创建的节目  多对多关系
        /// </summary>
        [HasAndBelongsToMany(typeof(ShowInfo),
            Table = "Show_TheHost_Info",
            ColumnKey = "TheHostInfoID",
            ColumnRef = "ShowInfoID",
            Cascade = ManyRelationCascadeEnum.All,
            Inverse = false,
            Lazy = false)]
        public IList<ShowInfo> Show { get; set; }

        #endregion
    }
}
