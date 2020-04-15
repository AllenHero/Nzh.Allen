using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("RoleAuthorize")]
    public class RoleAuthorizeModel
    {
        public int RoleId { get; set; }

        public int MenuId { get; set; }

        public int ButtonId { get; set; }
    }
}
