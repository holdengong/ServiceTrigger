using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ServiceTrigger.MultiTenancy.Dto;

namespace ServiceTrigger.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

