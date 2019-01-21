using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceTrigger.Projects.Dtos
{
    public class ProjectEditDto
    {
        public int? Id { get; set; }

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
