using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServiceTrigger.Projects
{
    public class Project : FullAuditedEntity
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [MaxLength(ServiceTriggerConsts.MaxNameLength)]
        public string ProjectName { get; set; }

        /// <summary>
        /// HostUrl地址
        /// </summary>
        [Required]
        [MaxLength(ServiceTriggerConsts.MaxUrlLength)]
        public string Host { get; set; }
    }
}
