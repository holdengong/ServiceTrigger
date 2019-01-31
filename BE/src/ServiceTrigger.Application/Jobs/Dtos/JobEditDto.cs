using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceTrigger.Jobs.Dtos
{
    public class JobEditDto
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(ServiceTriggerConsts.MaxNameLength)]
        public string JobName { get; set; }

        public string Enviroment { get; set; }

        [Required]
        public FrequencyEnum Frequency { get; set; }

        public bool IsEnable { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        public string ProjectName { get; set; }

        [Required]
        [MaxLength(ServiceTriggerConsts.MaxUrlLength)]
        public string ApiUrl { get; set; }

        [MaxLength(ServiceTriggerConsts.MaxNameLength)]
        public string Cron { get; set; }
    }
}
