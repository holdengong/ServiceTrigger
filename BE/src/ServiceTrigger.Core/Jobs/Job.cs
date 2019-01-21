﻿using Abp.Domain.Entities.Auditing;
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

        /// <summary>
        /// 项目ID
        /// </summary>
        [Required]
        public int ProjectId { get; set; }
    }

    public enum FrequencyEnum
    {
        Minutely = 10,
        Hourly = 20,
        Daily = 30,
        Monthly = 40
    }
}
