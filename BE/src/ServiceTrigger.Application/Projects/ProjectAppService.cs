using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ServiceTrigger.Authorization;
using ServiceTrigger.Jobs.Dtos;
using ServiceTrigger.Projects;
using ServiceTrigger.Projects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTrigger.Jobs
{
    [AbpAuthorize(PermissionNames.Projects_View)]
    public class ProjectAppService : ServiceTriggerAppServiceBase, IProjectAppService
    {
        private readonly IRepository<Project,int> _projectRepository;
        private readonly IRepository<Job, int> _jobRepository;

        public ProjectAppService(IRepository<Project, int> ProjectRepository, IRepository<Job, int> JobRepository)
        {
            _projectRepository = ProjectRepository;
            _jobRepository = JobRepository;
        }

        [AbpAuthorize(PermissionNames.Projects_Save)]
        public async Task Create(ProjectEditDto input)
        {
            if (await _projectRepository.CountAsync(e => e.ProjectName == input.ProjectName) > 0)
            {
                throw new UserFriendlyException("该项目名称已存在");
            }

            //TODO:新增前的逻辑判断，是否允许新增
            var entity = ObjectMapper.Map<Project>(input);

            await _projectRepository.InsertAsync(entity);
        }

        /// <summary>
        ///     编辑Person
        /// </summary>
        [AbpAuthorize(PermissionNames.Projects_Save)]
        public async Task Update(ProjectEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            var entity = await _projectRepository.GetAsync(input.Id.Value);

            entity = ObjectMapper.Map(input, entity);

            // ObjectMapper.Map(input, entity);
            await _projectRepository.UpdateAsync(entity);
        }

        [AbpAuthorize(PermissionNames.Projects_Delete)]
        public async Task Delete(EntityDto<int> entity)
        {
            //删除前的逻辑，是否允许删除
            var jobCount = await _jobRepository.CountAsync(e => e.Project.Id == entity.Id);

            if (jobCount > 0)
            {
                throw new UserFriendlyException("该项目下存在定时任务，不允许删除！");
            }

            await _projectRepository.DeleteAsync(entity.Id);
        }

        [AbpAuthorize(PermissionNames.Projects_View)]
        public async Task<ProjectListDto> Get(EntityDto<int> input)
        {
            var entity = await _projectRepository.GetAsync(input.Id);

            return ObjectMapper.Map<ProjectListDto>(entity);
        }

        [AbpAuthorize(PermissionNames.Projects_View)]
        public async Task<PagedResultDto<ProjectListDto>> GetAll(GetProjectInput input)
        {
            var query = _projectRepository.GetAll();

            var jobsCount = await query.CountAsync();

            var jobs = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var dtos = ObjectMapper.Map<List<ProjectListDto>>(jobs);

            return new PagedResultDto<ProjectListDto>(jobsCount, dtos);
        }
    }
}
