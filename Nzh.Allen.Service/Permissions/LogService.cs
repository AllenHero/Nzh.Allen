using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Service
{
    public class LogService : BaseService<LogModel>, ILogService
    {
        public dynamic GetListByFilter(LogModel filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.Account))
            {
                _where += " and Account=@Account";
            }
            if (!string.IsNullOrEmpty(filter.RealName))
            {
                _where += " and RealName=@RealName";
            }
            _where = CreateTimeWhereStr(filter.StartEndDate, _where);
            return GetListByFilter(filter, pageInfo, _where);
        }

        /// <summary>
        /// 写入登录日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int WriteDbLog(LogModel model, string ip, string iPAddressName)
        {
            model.IPAddress = ip;
            model.IPAddressName = iPAddressName;
            model.CreateTime = DateTime.Now;
            return BaseRepository.Insert(model);
        }
    }
}
