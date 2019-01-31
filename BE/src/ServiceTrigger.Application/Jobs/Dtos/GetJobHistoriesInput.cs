using Abp;
using Abp.Runtime.Validation;
using ServiceTrigger.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTrigger.Jobs.Dtos
{
    public class GetJobHistoriesInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int ProjectId { get; set; }
        public int JobId { get; set; }
        public string Keywords { get; set; }
        public bool? IsSuccess { get; set; } = null;
        public void Normalize()
        {
            if (string.IsNullOrWhiteSpace(Sorting))
            {
                Sorting = "Id";
            }
        }
    }
}
