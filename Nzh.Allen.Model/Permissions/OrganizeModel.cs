using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("Organize")]
    public class OrganizeModel : Entity
    {
        public int ParentId { get; set; }

        public string EnCode { get; set; }

        public string FullName { get; set; }

        public int CategoryId { get; set; }

        [Computed]
        public string CategoryName { get; set; }
    }
}
