using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("Items")]
    public class ItemsModel : Entity
    {
        public int ParentId { get; set; }

        public string EnCode { get; set; }

        public string FullName { get; set; }
    }
}
