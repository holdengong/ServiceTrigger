using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Threading.BackgroundWorkers;
using Abp.UI;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using ServiceTrigger.Jobs.Dtos;
using ServiceTrigger.Projects;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;

namespace ServiceTrigger.Jobs
{
    public class JobAppService : ServiceTriggerAppServiceBase, IJobAppService
    {
        private readonly IRepository<Job,int> _jobRepository;
        private readonly IRepository<Project, int> _projectRepository;
        private readonly IBackgroundWorkerManager _backgroundWorkerManager;

        public JobAppService(IRepository<Job, int> jobRepository,IRepository<Project,int> projectRepository, IBackgroundWorkerManager backgroundWorkerManager)
        {
            _jobRepository = jobRepository;
            _projectRepository = projectRepository;
            _backgroundWorkerManager = backgroundWorkerManager;
        }

        public async Task Create(JobEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增
            var entity = ObjectMapper.Map<Job>(input);

            var project = await _projectRepository.FirstOrDefaultAsync(e=>e.ProjectName==input.ProjectName);

            if (project == null)
            {
                throw new UserFriendlyException("不存在该项目"); 
            }

            entity.Project = project;
            entity.Id = await _jobRepository.InsertAndGetIdAsync(entity);

            RegisterJobInHangfire(entity);
        }

        /// <summary>
        ///     编辑Person
        /// </summary>
        public async Task Update(JobEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            var entity = await _jobRepository.GetAsync(input.Id.Value);

            entity = ObjectMapper.Map(input, entity);

            var project = await _projectRepository.FirstOrDefaultAsync(e => e.ProjectName == input.ProjectName);

            if (project == null)
            {
                throw new UserFriendlyException("不存在该项目");
            }

            entity.Project = project;

            await _jobRepository.UpdateAsync(entity);

            RegisterJobInHangfire(entity);
        }

        public async Task Delete(EntityDto<int> entity)
        {
            await _jobRepository.DeleteAsync(entity.Id);

            RecurringJob.RemoveIfExists(entity.Id.ToString());
        }

        public async Task<JobListDto> Get(EntityDto<int> input)
        {
            var entity = await _jobRepository.GetAsync(input.Id);

            return ObjectMapper.Map<JobListDto>(entity);
        }

        public async Task<PagedResultDto<JobListDto>> GetAll(GetJobInput input)
        {
            var query = _jobRepository.GetAllIncluding(e=>e.Project).OrderBy(input.Sorting).PageBy(input);

            var jobsCount = await query.CountAsync();

            var jobs = await query.ToListAsync();

            var dtos = new List<JobListDto>();

            jobs.ForEach(j=> {
                var dto = ObjectMapper.Map<JobListDto>(j);
                dto.ProjectName = j.Project.ProjectName;
                dtos.Add(dto);
            });

            return new PagedResultDto<JobListDto>(jobsCount, dtos);
        }

        public async Task UpdateStatus(UpdateJobStatusInput input)
        {
            var job = await _jobRepository.GetAsync(input.Id);

            if (job != null)
            {
                job.IsEnable = input.IsEnable;
                await _jobRepository.UpdateAsync(job);
            }
        }

        protected async void RegisterJobInHangfire(Job entity)
        {
            var project = await _projectRepository.GetAsync(entity.Project.Id);

            if (project == null)
            {
                throw new UserFriendlyException("项目不存在");
            }

            var projectHost = project.Host;


            SendRequestJobArgs args = new SendRequestJobArgs()
            {
                Host = projectHost,
                ApiUrl = entity.ApiUrl
            };

            string cron = Cron.Daily();
            switch (entity.Frequency)
            {
                case FrequencyEnum.Minutely:
                    cron = Cron.Minutely();
                    break;
                case FrequencyEnum.Hourly:
                    cron = Cron.Hourly();
                    break;
                case FrequencyEnum.Daily:
                    cron = Cron.Daily();
                    break;
                case FrequencyEnum.Monthly:
                    cron = Cron.Monthly();
                    break;
                default:
                    break;
            }

            RecurringJob.AddOrUpdate<SendRequestJob>(entity.Id.ToString(), e => e.Execute(args), cron);
        }
    }
}
