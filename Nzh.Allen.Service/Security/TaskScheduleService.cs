using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Service
{
    public class TaskScheduleService : BaseService<TaskScheduleModel>, ITaskScheduleService
    {

        public dynamic GetListByFilter(TaskScheduleModel filter, PageInfo pageInfo)
        {
            pageInfo.prefix = "a.";
            string _where = " taskschedule a where a.State in (0,1)";
            pageInfo.returnFields = "*";
            return GetPageUnite(filter, pageInfo, _where);
        }

        public bool ResumeScheduleJob(TaskScheduleModel sm)
        {
            return UpdateById(sm);
        }

        public bool StopScheduleJob(TaskScheduleModel sm)
        {
            return UpdateById(sm);
        }
    }
}
