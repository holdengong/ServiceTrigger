﻿using Abp.Application.Services.Dto;
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
using ServiceTrigger.JobHistories;
using ServiceTrigger.Jobs.Dtos;
using ServiceTrigger.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceTrigger.Jobs
{
    [AbpAuthorize(PermissionNames.Jobs_View)]
    public class JobAppService : ServiceTriggerAppServiceBase, IJobAppService
    {
        private readonly IRepository<Job,int> _jobRepository;
        private readonly IRepository<Project, int> _projectRepository;
        private readonly IRepository<Hash, int> _hashRepository;
        private readonly IRepository<JobHistory, int> _jobHistoryRepository;

        private readonly IBackgroundWorkerManager _backgroundWorkerManager;

        public JobAppService(IRepository<Job, int> jobRepository
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

        [AbpAuthorize(PermissionNames.Jobs_Save)]
        public async Task Create(JobEditDto input)
        {
            if (input.Frequency == FrequencyEnum.自定义 && string.IsNullOrWhiteSpace(input.Cron))
            {
                throw new UserFriendlyException("必须选择调度频率或者填写自定义Cron表达式");
            }

            var entity = ObjectMapper.Map<Job>(input);

            var project = await _projectRepository.FirstOrDefaultAsync(e => e.ProjectName == input.ProjectName && e.Enviroment == input.Enviroment);
            entity.Project = project ?? throw new UserFriendlyException("不存在该项目环境");
            entity.Id = await _jobRepository.InsertAndGetIdAsync(entity);

            if (!string.IsNullOrWhiteSpace(input.Cron))
            {
                input.Frequency = FrequencyEnum.自定义;
            }

            TestApiConnection(project.Host, entity.ApiUrl);

            RegisterJobInHangfire(entity);
        }

        /// <summary>
        ///     编辑Person
        /// </summary>
        [AbpAuthorize(PermissionNames.Jobs_Save)]
        public async Task Update(JobEditDto input)
        {
            if (input.Frequency == FrequencyEnum.自定义 && string.IsNullOrWhiteSpace(input.Cron))
            {
                throw new UserFriendlyException("必须选择调度频率或者填写自定义Cron表达式");
            }

            var entity = await _jobRepository.GetAsync(input.Id.Value);

            entity = ObjectMapper.Map(input, entity);

            var project = await _projectRepository.FirstOrDefaultAsync(e => e.ProjectName == input.ProjectName);

            if (project == null)
            {
                throw new UserFriendlyException("不存在该项目");
            }

            entity.Project = project;

            if (!string.IsNullOrWhiteSpace(input.Cron))
            {
                entity.Frequency = FrequencyEnum.自定义;
            }

            await _jobRepository.UpdateAsync(entity);

            TestApiConnection(project.Host, entity.ApiUrl);

            RegisterJobInHangfire(entity);
        }

        [AbpAuthorize(PermissionNames.Jobs_Delete)]
        public async Task Delete(EntityDto<int> entity)
        {
            await _jobRepository.DeleteAsync(entity.Id);

            RecurringJob.RemoveIfExists(entity.Id.ToString());
        }

        [AbpAuthorize(PermissionNames.Jobs_View)]
        public async Task<JobListDto> Get(EntityDto<int> input)
        {
            var entity = await _jobRepository.GetAsync(input.Id);

            var dto = ObjectMapper.Map<JobListDto>(entity);

            return dto;
        }

        //[AbpAuthorize(PermissionNames.Jobs_View)]
        [AbpAllowAnonymous]
        public async Task<PagedResultDto<JobListDto>> GetAll(GetJobInput input)
        {
            var query = _jobRepository.GetAllIncluding(e=>e.Project).OrderBy(input.Sorting).PageBy(input);

            var jobsCount = await query.CountAsync();

            var jobs = await query.ToListAsync();

            var dtos = new List<JobListDto>();

            var hashtest = _hashRepository.GetAllList();

            jobs.ForEach(j => {
                var dto = ObjectMapper.Map<JobListDto>(j);

                var hashsQuery = _hashRepository.GetAll();

                hashsQuery = hashsQuery.Where(h => h.Key == $"{AppConsts.RecurringJobPrefix}:{j.Id}");

                var hashs = hashsQuery.ToList();

                if (hashs != null)
                {
                    dto.Job = hashs.SingleOrDefault(e => e.Field == "Job")?.Value;
                    //dto.Cron = hashs.SingleOrDefault(e => e.Field == "Cron")?.Value;
                    dto.TimeZoneId = hashs.SingleOrDefault(e => e.Field == "TimeZoneId")?.Value;
                    dto.Queue = hashs.SingleOrDefault(e => e.Field == "Queue")?.Value;

                    if (hashs.SingleOrDefault(e => e.Field == "CreatedAt") != null)
                    {
                        dto.CreatedAt = DateTime.Parse(hashs.SingleOrDefault(e => e.Field == "CreatedAt").Value);
                    }

                    if (hashs.SingleOrDefault(e => e.Field == "LastExecution") != null)
                    {
                        dto.LastExecution = DateTime.Parse(hashs.SingleOrDefault(e => e.Field == "LastExecution").Value);
                    }

                    if (hashs.SingleOrDefault(e => e.Field == "LastJobId") != null)
                    {
                        dto.LastJobId = int.Parse(hashs.SingleOrDefault(e => e.Field == "LastJobId").Value);
                    }

                    if (hashs.SingleOrDefault(e => e.Field == "NextExecution") != null)
                    {
                        dto.NextExecution = DateTime.Parse(hashs.SingleOrDefault(e => e.Field == "NextExecution").Value);
                    }

                    if (!string.IsNullOrWhiteSpace(j.Cron))
                    {
                        CronExpressionDescriptor.Options opt = new CronExpressionDescriptor.Options()
                        {
                            DayOfWeekStartIndexZero = false,
                            ThrowExceptionOnParseError = false
                        };
                        dto.CronDescription = CronExpressionDescriptor.ExpressionDescriptor.GetDescription(dto.Cron, opt);
                    }
                }

                dto.ProjectName = j.Project.ProjectName;
                dto.Enviroment = j.Project.Enviroment;
                dtos.Add(dto);
            });

            return new PagedResultDto<JobListDto>(jobsCount, dtos);
        }

        [AbpAuthorize(PermissionNames.Jobs_Enable)]
        public async Task Enable(EntityDto<int> entity)
        {
            var job = _jobRepository.GetAllIncluding(p => p.Project).FirstOrDefault(j => j.Id == entity.Id);

            if (job == null || job.Project == null)
            {
                throw new UserFriendlyException("定时任务信息不完整");
            }

            job.IsEnable = true;
            await _jobRepository.UpdateAsync(job);
            RegisterJobInHangfire(job);
        }

        [AbpAuthorize(PermissionNames.Jobs_Disable)]
        public async Task Disable(EntityDto<int> entity)
        {
            var job = await _jobRepository.GetAsync(entity.Id);

            if (job != null)
            {
                job.IsEnable = false;
                await _jobRepository.UpdateAsync(job);
            }

            RecurringJob.RemoveIfExists(entity.Id.ToString());
        }

        [AbpAuthorize(PermissionNames.Jobs_Trigger)]
        public void Trigger(EntityDto<int> entity)
        {
            RecurringJob.Trigger(entity.Id.ToString());
        }

        public void TestApiConnection(string host,string apiUrl)
        {
            HttpClient hc = new HttpClient();
            var url = host.EnsureEndsWith('/') + apiUrl.Trim('/');
            
            try
            {
                var response = hc.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new UserFriendlyException($"请求失败：{response.StatusCode}_{response.Content.ReadAsStringAsync().Result}");
                }
            }
            catch (Exception)
            {
                throw new UserFriendlyException($"请求失败：{url}");
            }
        }

        protected async void RegisterJobInHangfire(Job entity)
        {
            if (entity == null || entity.Project == null)
            {
                throw new UserFriendlyException("定时任务信息不完整");
            }

            var projectHost = entity.Project.Host;

            SendRequestJobArgs args = new SendRequestJobArgs()
            {
                JobId = entity.Id,
                Host = projectHost,
                ApiUrl = entity.ApiUrl
            };

            string cron = Cron.Daily();
            switch (entity.Frequency)
            {
                case FrequencyEnum.每分钟:
                    cron = Cron.Minutely();
                    break;
                case FrequencyEnum.每小时:
                    cron = Cron.Hourly();
                    break;
                case FrequencyEnum.每天:
                    cron = Cron.Daily();
                    break;
                case FrequencyEnum.每月:
                    cron = Cron.Monthly();
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(entity.Cron))
            {
                cron = entity.Cron;
            }

            RecurringJob.AddOrUpdate<SendRequestJob>(entity.Id.ToString(), e => e.Execute(args), cron, TimeZoneInfo.Local);
        }
    }
}
