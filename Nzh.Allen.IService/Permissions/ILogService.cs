using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface ILogService : IBaseService<LogModel>
    {
        int WriteDbLog(LogModel model, string ip, string iPAddressName);
    }
}
