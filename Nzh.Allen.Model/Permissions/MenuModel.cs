using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("Menu")]
    public class MenuModel : Entity
    {

        public int ParentId { get; set; }

        public string FullName { get; set; }

        public string FontFamily { get; set; }

        public string Icon { get; set; }

        public string UrlAddress { get; set; }

        [Computed]
        public string MenuButtonHtml { get; set; }

        [Computed]
        public bool IsChecked { get; set; }
    }
}
