﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    public class ModifyPwd
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string Repassword { get; set; }
    }
}
