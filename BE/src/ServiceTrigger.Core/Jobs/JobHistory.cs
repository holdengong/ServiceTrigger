using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text;

namespace ServiceTrigger.Jobs
{
    [Table("st_JobHistory")]
    public class JobHistory: FullAuditedEntity
    {
        public int JobId { get; set; }

        [Required]
        public string RequestUrl { get; set; }
        public string ResultString { get; set; }

        public string ErrorMsg { get; set; }

        [Required]
        public bool Result { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
