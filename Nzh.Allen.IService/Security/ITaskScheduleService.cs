using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface ITaskScheduleService : IBaseService<TaskScheduleModel>
    {
        bool ResumeScheduleJob(TaskScheduleModel sm);
        bool StopScheduleJob(TaskScheduleModel sm);
    }
}
