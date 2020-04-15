using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("Menu")]
    public class MenuModel : Entity
    {
        /// <summary>
        /// 父级
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 字体类型 layui-icon|ok-icon|my-icon
        /// </summary>
        public string FontFamily { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string UrlAddress { get; set; }

        /// <summary>
        /// 菜单按钮复选框Html
        /// </summary>
        [Computed]
        public string MenuButtonHtml { get; set; }
        /// <summary>
        /// 菜单是否选中
        /// </summary>
        [Computed]
        public bool IsChecked { get; set; }
    }
}
