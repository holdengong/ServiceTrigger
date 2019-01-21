using Abp.Application.Services.Dto;

namespace ServiceTrigger.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

