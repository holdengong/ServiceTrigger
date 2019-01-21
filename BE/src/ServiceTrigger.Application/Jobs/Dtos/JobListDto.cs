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
        public FrequencyEnum Frequency { get; set; }
        public bool IsEnable { get; set; }
    }
}
