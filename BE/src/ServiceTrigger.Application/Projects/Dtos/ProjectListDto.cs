using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTrigger.Projects.Dtos
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectListDto : FullAuditedEntityDto
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// HostUrl地址
        /// </summary>
        public string Host { get; set; }
    }
}
