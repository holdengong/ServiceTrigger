using Abp.Domain.Entities.Auditing;
using ServiceTrigger.JobHistories;
using ServiceTrigger.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServiceTrigger.Jobs
{
    public class Job : FullAuditedEntity
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        [Required]
        [MaxLength(ServiceTriggerConsts.MaxNameLength)]
        public string JobName { get; set; }

        /// <summary>
        /// 触发频率
        /// </summary>
        [Required]
        public FrequencyEnum Frequency { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 调用的API地址（相对地址）
        /// </summary>
        [Required]
        [MaxLength(ServiceTriggerConsts.MaxUrlLength)]
        public string ApiUrl { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public List<JobHistory> JobHistories { get; set; }

        [MaxLength(ServiceTriggerConsts.MaxNameLength)]
        public string Cron { get; set; }
    }

    public enum FrequencyEnum
    {
        自定义 = 0,
        每分钟 = 10,
        每小时 = 20,
        每天 = 30,
        每月 = 40
    }
}
