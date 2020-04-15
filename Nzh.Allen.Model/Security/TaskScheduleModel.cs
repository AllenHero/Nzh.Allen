using Nzh.Allen.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    [Table("taskschedule")]
    public class TaskScheduleModel
    {
        [Key(true)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public string CronExpress { get; set; }
        public DateTime StarRunTime { get; set; }
        public DateTime? EndRunTime { get; set; }
        public DateTime? CreateTime { get; set; }
        public int State { get; set; }
    }
}
