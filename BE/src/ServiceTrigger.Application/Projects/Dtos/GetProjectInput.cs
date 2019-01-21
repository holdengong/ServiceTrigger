using Abp;
using Abp.Runtime.Validation;
using ServiceTrigger.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTrigger.Projects.Dtos
{
    public class GetProjectInput : PagedAndSortedInputDto, IShouldNormalize
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
