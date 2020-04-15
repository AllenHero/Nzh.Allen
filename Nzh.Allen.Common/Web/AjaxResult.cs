using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Common
{
    public class AjaxResult
    {
        public object state { get; set; }

        public string message { get; set; }

        public object data { get; set; }
    }

    public enum ResultType
    {
        /// <summary>
        /// 消息结果类型
        /// </summary>
        info,

        /// <summary>
        /// 成功结果类型
        /// </summary>
        success,

        /// <summary>
        /// 警告结果类型
        /// </summary>
        warning,

        /// <summary>
        /// 异常结果类型
        /// </summary>
        error
    }
}
