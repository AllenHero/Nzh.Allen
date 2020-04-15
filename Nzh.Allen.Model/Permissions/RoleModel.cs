using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("Role")]
    public class RoleModel : Entity
    {
        public string EnCode { get; set; }

        public string FullName { get; set; }

        public int TypeClass { get; set; }

        [Computed]
        public string TypeName { get; set; }
    }
}
