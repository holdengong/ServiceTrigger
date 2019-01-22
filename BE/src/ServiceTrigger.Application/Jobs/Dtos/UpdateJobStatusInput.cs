using Abp;
using Abp.Runtime.Validation;
using ServiceTrigger.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTrigger.Jobs.Dtos
{
    public class UpdateJobStatusInput
    {
        public int Id { get; set; }
        public bool IsEnable { get; set; }
    }
}
