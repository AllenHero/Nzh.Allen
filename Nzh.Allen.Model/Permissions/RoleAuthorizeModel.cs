﻿using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("RoleAuthorize")]
    public class RoleAuthorizeModel
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 模块主键
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 按钮主键
        /// </summary>
        public int ButtonId { get; set; }
    }
}
