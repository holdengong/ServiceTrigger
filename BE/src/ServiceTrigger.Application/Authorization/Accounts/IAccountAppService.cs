using System.Threading.Tasks;
using Abp.Application.Services;
using ServiceTrigger.Authorization.Accounts.Dto;

namespace ServiceTrigger.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
