using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ServiceTrigger.Jobs.Dtos;
using ServiceTrigger.Projects.Dtos;
using System.Threading.Tasks;

namespace ServiceTrigger.Projects
{
    public interface IProjectAppService : IApplicationService
    {
        /// <summary>
        /// 获取项目分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ProjectListDto>> GetPagedProjectAsync(GetProjectInput input);

        /// <summary>
        /// 根据ID获取项目信息
        /// </summary>
        /// <returns></returns>
        Task<ProjectListDto> GetProjectByIdAsync(EntityDto<int> input);

        /// <summary>
        /// 新增或更改项目信息
        /// </summary>
        /// <returns></returns>
        Task CreateOrUpdateProjectAsync(CreateOrUpdateProjectInput dto);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteProjectAsync(EntityDto<int> entity);
    }
}
