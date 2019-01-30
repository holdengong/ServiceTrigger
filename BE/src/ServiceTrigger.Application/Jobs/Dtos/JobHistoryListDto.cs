using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ServiceTrigger.JobHistories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ServiceTrigger.Jobs.Dtos
{
    [AutoMapFrom(typeof(JobHistory))]
    public class JobHistoryListDto: FullAuditedEntityDto
    {
        public bool Result { get; set; }
        public string ResultString { get; set; }
        public string ErrorMsg { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public JobListDto Job { get; set; }
    }
}
