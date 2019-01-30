using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Threading.BackgroundWorkers;
using Abp.UI;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using ServiceTrigger.Authorization;
using ServiceTrigger.Hangfire;
using ServiceTrigger.Jobs;
using ServiceTrigger.Jobs.Dtos;
using ServiceTrigger.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceTrigger.JobHistories
{
    public class JobHistoryAppService : ServiceTriggerAppServiceBase, IJobHistoryAppService
    {
        private readonly IRepository<Job,int> _jobRepository;
        private readonly IRepository<Project, int> _projectRepository;
        private readonly IRepository<Hash, int> _hashRepository;
        private readonly IRepository<JobHistory, int> _jobHistoryRepository;

        private readonly IBackgroundWorkerManager _backgroundWorkerManager;

        public JobHistoryAppService(IRepository<Job, int> jobRepository
            , IRepository<Project, int> projectRepository
            , IBackgroundWorkerManager backgroundWorkerManager
            , IRepository<Hash, int> hashRepository
            , IRepository<JobHistory, int> jobHistoryRepository
            )
        {
            _jobRepository = jobRepository;
            _projectRepository = projectRepository;
            _backgroundWorkerManager = backgroundWorkerManager;
            _hashRepository = hashRepository;
            _jobHistoryRepository = jobHistoryRepository;
        }

        [AbpAllowAnonymous]
        public async Task<PagedResultDto<JobHistoryListDto>> GetAll(GetJobHistoriesInput input)
        {
            var query = _jobHistoryRepository.GetAll()
                .Include(h => h.Job)
                .ThenInclude(j => j.Project)
                .WhereIf(input.JobId > 0, h => h.JobId == input.JobId)
                .WhereIf(input.ProjectId > 0, h => h.Job.ProjectId == input.ProjectId);

            //TODO:根据传入的参数添加过滤条件
            var historyCount = await query.CountAsync();

            var histories = await query
                .OrderBy(input.Sorting).AsNoTracking()
                .OrderByDescending(h => h.CreationTime)
                .PageBy(input)
                .ToListAsync();

            var historyListDtos = ObjectMapper.Map<List<JobHistoryListDto>>(histories);

            var pagedResult = new PagedResultDto<JobHistoryListDto>(
                historyCount,
                historyListDtos
            );

            return pagedResult;
        }
    }
}
