using Abp;
using Abp.Runtime.Validation;
using ServiceTrigger.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTrigger.Jobs.Dtos
{
    public class GetJobInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string FilterText { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrWhiteSpace(Sorting))
            {
                Sorting = "Id";
            }
        }
    }
}
