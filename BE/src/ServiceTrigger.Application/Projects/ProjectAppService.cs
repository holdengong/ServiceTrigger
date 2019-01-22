using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
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
    public class ProjectAppService : ServiceTriggerAppServiceBase, IProjectAppService
    {
        private readonly IRepository<Project,int> _projectRepository;
        private readonly IRepository<Job, int> _jobRepository;

        public ProjectAppService(IRepository<Project, int> ProjectRepository, IRepository<Job, int> JobRepository)
        {
            _projectRepository = ProjectRepository;
            _jobRepository = JobRepository;
        }

        public async Task CreateOrUpdateProjectAsync(CreateOrUpdateProjectInput input)
        {
            if (input.Project.Id.HasValue && input.Project.Id > 0)
            {
                await UpdateProjectAsync(input.Project);
            }
            else
            {
                await CreateProjectAsync(input.Project);
            }
        }

        protected virtual async Task CreateProjectAsync(ProjectEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增
            var entity = ObjectMapper.Map<Project>(input);

            await _projectRepository.InsertAsync(entity);
        }

        /// <summary>
        ///     编辑Person
        /// </summary>
        protected virtual async Task UpdateProjectAsync(ProjectEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            var entity = await _projectRepository.GetAsync(input.Id.Value);

            entity = ObjectMapper.Map<Project>(input);

            // ObjectMapper.Map(input, entity);
            await _projectRepository.UpdateAsync(entity);
        }

        public async Task DeleteProjectAsync(EntityDto<int> entity)
        {
            //删除前的逻辑，是否允许删除
            var jobCount = await _jobRepository.CountAsync(e => e.ProjectId == entity.Id);

            if (jobCount > 0)
            {
                throw new UserFriendlyException("该项目下存在定时任务，不允许删除！");
            }

            await _projectRepository.DeleteAsync(entity.Id);
        }

        public async Task<ProjectListDto> GetProjectByIdAsync(EntityDto<int> input)
        {
            var entity = await _projectRepository.GetAsync(input.Id);

            return ObjectMapper.Map<ProjectListDto>(entity);
        }

        public async Task<PagedResultDto<ProjectListDto>> GetPagedProjectAsync(GetProjectInput input)
        {
            var query = _projectRepository.GetAll();

            var jobsCount = await query.CountAsync();

            var jobs = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var dtos = ObjectMapper.Map<List<ProjectListDto>>(jobs);

            return new PagedResultDto<ProjectListDto>(jobsCount, dtos);
        }
    }
}
