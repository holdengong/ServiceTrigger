using System.Threading.Tasks;
using Abp.Application.Services;
using ServiceTrigger.Sessions.Dto;

namespace ServiceTrigger.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
