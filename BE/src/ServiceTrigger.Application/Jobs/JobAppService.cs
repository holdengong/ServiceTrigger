using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using ServiceTrigger.Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTrigger.Jobs
{
    public class JobAppService : ServiceTriggerAppServiceBase, IJobAppService
    {
        private readonly IRepository<Job,int> _jobRepository;

        public JobAppService(IRepository<Job, int> JobRepository)
        {
            _jobRepository = JobRepository;
        }

        public Task CreateOrUpdateJobAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteJobAsync(EntityDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<JobEditDto> GetJobByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<JobListDto>> GetPagedJobAsync(GetJobInput input)
        {
            var query = _jobRepository.GetAll();

            var jobsCount = await query.CountAsync();

            var jobs = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var dtos = jobs.MapTo<List<JobListDto>>();

            return new PagedResultDto<JobListDto>(jobsCount, dtos);
        }
    }
}
