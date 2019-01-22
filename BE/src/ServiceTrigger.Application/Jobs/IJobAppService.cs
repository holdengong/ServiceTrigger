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
        Task<PagedResultDto<JobListDto>> GetAll(GetJobInput input);

        /// <summary>
        /// 根据ID获取定时任务信息
        /// </summary>
        /// <returns></returns>
        Task<JobListDto> Get(EntityDto<int> input);

        /// <summary>
        /// 新增定时任务信息
        /// </summary>
        /// <returns></returns>
        Task Create(JobEditDto dto);

        /// <summary>
        /// 修改定时任务信息
        /// </summary>
        /// <returns></returns>
        Task Update(JobEditDto dto);

        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Delete(EntityDto<int> entity);

        /// <summary>
        /// 更新任务是否启用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateStatus(UpdateJobStatusInput input);
    }
}
