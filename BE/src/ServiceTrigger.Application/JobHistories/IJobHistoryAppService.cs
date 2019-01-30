using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using ServiceTrigger.Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTrigger.JobHistories
{
    public interface IJobHistoryAppService : IApplicationService
    {
        Task<PagedResultDto<JobHistoryListDto>> GetAll(GetJobHistoriesInput input);
    }
}
