using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTrigger.Jobs.Dtos
{
    [AutoMapFrom(typeof(Job))]
    public class JobListDto : FullAuditedEntityDto
    {
        public string JobName { get; set; }
        public string Frequency { get; set; }
        public bool IsEnable { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ApiUrl { get; set; }
        
        //hangfire hash表字段
        public string Job { get; set; }
        public string Cron { get; set; }
        public string TimeZoneId { get; set; }
        public string Queue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastExecution { get; set; }
        public int LastJobId { get; set; }
        public DateTime NextExecution { get; set; }

    }
}
