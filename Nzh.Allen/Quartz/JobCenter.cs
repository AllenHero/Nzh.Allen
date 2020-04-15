using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Nzh.Allen.Model;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Nzh.Allen.Quartz
{
    public class JobCenter
    {
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        private readonly ILogger<JobCenter> Logger;

        /// <summary>
        /// 任务计划
        /// </summary>
        private IScheduler scheduler = null;
        /// <summary>
        /// 是否初始化
        /// </summary>
        private bool IsStart = false;

        public JobCenter(ILogger<JobCenter> logger)
        {
            Logger = logger;
            Start();
            IsStart = true;

        }
        private void Start()
        {
            ISchedulerFactory schedf = new StdSchedulerFactory();
            scheduler = schedf.GetScheduler().Result;
            scheduler.Start().Wait();
            Logger.LogInformation($"启动Quartz");
        }

        /// <summary>
        /// 暂停指定任务计划
        /// </summary>
        /// <returns></returns>
        public void StopScheduleJobAsync(TaskScheduleModel sm)
        {
            if (!IsStart)
                throw new Exception("任务计划未初始化");
            var jk = new JobKey(sm.JobName, sm.JobGroup);
            if (scheduler.CheckExists(jk).Result)
            {
                //使任务暂停
                scheduler.PauseJob(jk).Wait();
                Logger.LogInformation($"暂停任务{sm.JobGroup}.{sm.JobName}");
            }
        }
        /// <summary>
        /// 恢复指定的任务计划**恢复的是暂停后的任务计划，如果没有指定任务就创建新的任务
        /// </summary>
        /// <returns></returns>
        public void ResumeScheduleJobAsync(TaskScheduleModel sm)
        {
            if (!IsStart)
                throw new Exception("任务计划未初始化");
            var jk = new JobKey(sm.JobName, sm.JobGroup);
            if (scheduler.CheckExists(jk).Result)
            {
                //有任务就恢复
                scheduler.ResumeJob(jk).Wait();
                Logger.LogInformation($"恢复任务{sm.JobGroup}.{sm.JobName}");
            }
            else
            {
                //没有任务就创建
                this.AddScheduleJobAsync(sm);
            }

        }


        /// <summary>
        /// 添加一个工作调度（映射程序集指定IJob实现类）
        /// </summary>
        /// <returns></returns>
        private void AddScheduleJobAsync(TaskScheduleModel m)
        {
            if (!IsStart)
                throw new Exception("任务计划未初始化");
            //检查任务是否已存在
            var jk = new JobKey(m.JobName, m.JobGroup);
            if (scheduler.CheckExists(jk).Result)
            {
                //删除已经存在任务
                scheduler.PauseJob(jk).Wait();//先暂停
                scheduler.DeleteJob(jk).Wait();//在删除
                Logger.LogInformation($"移除了老任务{m.JobGroup}.{m.JobName}");
            }
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(m.StarRunTime, 1);
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(m.EndRunTime, 1);
            //反射获取IJob实现
            string dllFilePath = Path.Combine(WebHostEnvironment.WebRootPath, "JobPlugins") + "/" + m.FileName;
            Assembly assembly = Assembly.LoadFile(dllFilePath);
            Type[] types = assembly.GetExportedTypes();
            Type typeIJob = typeof(IJob);
            Type jobType = null;
            for (int i = 0; i < types.Length; i++)
            {
                if (typeIJob.IsAssignableFrom(types[i]) && !types[i].IsAbstract)
                {
                    jobType = types[i];
                    break;
                }
            }
            IJobDetail job = JobBuilder.Create(jobType)
              .WithIdentity(m.JobName, m.JobGroup)
              .Build();
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                         .StartAt(starRunTime)
                                         .EndAt(endRunTime)
                                         .WithIdentity(m.JobName, m.JobGroup)
                                         .WithCronSchedule(m.CronExpress)
                                         .Build();
            scheduler.ScheduleJob(job, trigger).Wait();
            Logger.LogInformation($"创建新任务{m.JobGroup}.{m.JobName}");
        }

    }
}
