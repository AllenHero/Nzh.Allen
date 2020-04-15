using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("User")]
    public class UserModel : Entity
    {
        public string Account { get; set; }

        public string UserPassword { get; set; }

        public string RealName { get; set; }

        public string HeadIcon { get; set; }

        public int Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string MobilePhone { get; set; }

        public string Email { get; set; }

        public string WeChat { get; set; }

        public int DepartmentId { get; set; }

        public int RoleId { get; set; }

        public int? EnabledMark { get; set; }

        [Computed]
        public override int SortCode { get; set; }

        [Computed]
        public string DepartmentName { get; set; }

        [Computed]
        public string RoleName { get; set; }
    }
}
