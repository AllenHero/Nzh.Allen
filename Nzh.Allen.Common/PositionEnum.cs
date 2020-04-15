using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nzh.Allen.Common
{
    public enum PositionEnum
    {
        /// <summary>
        /// 表内
        /// </summary>
        [Display(Name = "表内")]
        FormInside = 0,
        /// <summary>
        /// 表外
        /// </summary>
        [Display(Name = "表外")]
        FormRightTop = 1
    }
}
