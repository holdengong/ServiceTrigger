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

        public async Task CreateOrUpdateJobAsync(CreateOrUpdateJobInput input)
        {
            if (input.Job.Id.HasValue && input.Job.Id > 0)
            {
                await UpdateJobAsync(input.Job);
            }
            else
            {
                await CreateJobAsync(input.Job);
            }
        }

        protected virtual async Task CreateJobAsync(JobEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增
            var entity = input.MapTo<Job>();

            await _jobRepository.InsertAsync(entity);
        }

        /// <summary>
        ///     编辑Person
        /// </summary>
        protected virtual async Task UpdateJobAsync(JobEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            var entity = await _jobRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _jobRepository.UpdateAsync(entity);
        }

        public async Task DeleteJobAsync(EntityDto<int> entity)
        {
            //删除前的逻辑，是否允许删除

            await _jobRepository.DeleteAsync(entity.Id);
        }

        public async Task<JobListDto> GetJobByIdAsync(EntityDto<int> input)
        {
            var entity = await _jobRepository.GetAsync(input.Id);

            return entity.MapTo<JobListDto>();
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
