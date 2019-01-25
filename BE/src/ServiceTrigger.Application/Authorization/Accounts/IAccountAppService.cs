using System.Threading.Tasks;
using Abp.Application.Services;
using ServiceTrigger.Authorization.Accounts.Dto;

namespace ServiceTrigger.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);

        /// <summary>
        /// 检查用户是否有某权限，允许匿名访问，供外部cookie token授权接入
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        Task<bool> CheckUserPermissionByUserId(long userId, string permissionName);
    }
}
