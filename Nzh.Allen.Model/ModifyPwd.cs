using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    public class ModifyPwd
    {
        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }

        public string Repassword { get; set; }
    }
}
