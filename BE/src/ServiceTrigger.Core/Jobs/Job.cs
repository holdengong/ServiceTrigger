using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServiceTrigger.Jobs
{
    public class Job : FullAuditedEntity
    {
        [Required]
        [MaxLength(ServiceTriggerConsts.MaxNameLength)]
        public string JobName { get; set; }

        [Required]
        public FrequencyEnum Frequency { get; set; }

        [Required]
        public bool IsEnable { get; set; }
    }

    public enum FrequencyEnum
    {
        Minutely = 10,
        Hourly = 20,
        Daily = 30,
        Monthly = 40
    }
}
