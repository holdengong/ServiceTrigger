﻿using System;
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

        [Required]
        public FrequencyEnum Frequency { get; set; }

        [Required]
        public bool IsEnable { get; set; }
    }
}
