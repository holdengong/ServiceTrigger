using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTrigger.Jobs.Dtos
{
    public class JobEditDto
    {
        public string JobName { get; set; }
        public FrequencyEnum Frequency { get; set; }
        public bool IsEnable { get; set; }
    }
}
