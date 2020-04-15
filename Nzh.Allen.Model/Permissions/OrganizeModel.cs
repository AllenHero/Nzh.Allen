using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("Organize")]
    public class OrganizeModel : Entity
    {
        /// <summary>
        /// 父级
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 机构分类
        /// </summary>
        [Computed]
        public string CategoryName { get; set; }
    }
}
