using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("ItemsDetail")]
    public class ItemsDetailModel : Entity
    {
        public int ItemId { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        [Computed]
        public string ItemType { get; set; }
    }
}
