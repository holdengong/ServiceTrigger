using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ServiceTrigger.Controllers
{
    public abstract class ServiceTriggerControllerBase: AbpController
    {
        protected ServiceTriggerControllerBase()
        {
            LocalizationSourceName = ServiceTriggerConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
