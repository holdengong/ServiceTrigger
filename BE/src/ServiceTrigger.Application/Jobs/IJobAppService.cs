using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using ServiceTrigger.Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTrigger.Jobs
{
    public interface IJobAppService : IApplicationService
    {
        /// <summary>
        /// 获取定时任务分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<JobListDto>> GetPagedJobAsync(GetJobInput input);

        /// <summary>
        /// 根据ID获取定时任务信息
        /// </summary>
        /// <returns></returns>
        Task<JobListDto> GetJobByIdAsync(EntityDto<int> input);

        /// <summary>
        /// 新增或更改定时任务信息
        /// </summary>
        /// <returns></returns>
        Task CreateOrUpdateJobAsync(CreateOrUpdateJobInput dto);

        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteJobAsync(EntityDto<int> entity);

        /// <summary>
        /// 更新任务是否启用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateJobStatus(UpdateJobStatusInput input);
    }
}
