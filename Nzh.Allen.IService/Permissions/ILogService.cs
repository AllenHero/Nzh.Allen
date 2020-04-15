using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface ILogService : IBaseService<LogModel>
    {
        /// <summary>
        /// 写入登录日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int WriteDbLog(LogModel model, string ip, string iPAddressName);

    }
}
