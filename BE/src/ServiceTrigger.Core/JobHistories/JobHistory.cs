using Abp.Domain.Entities.Auditing;
using ServiceTrigger.Jobs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text;

namespace ServiceTrigger.JobHistories
{
    [Table("st_JobHistory")]
    public class JobHistory: FullAuditedEntity
    {
        [Required]
        public string RequestUrl { get; set; }
        public string ResultString { get; set; }

        public string ErrorMsg { get; set; }

        [Required]
        public bool Result { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
