using Abp.AspNetCore.Mvc.Controllers;

namespace ServiceTrigger.Web.Controllers
{
    public abstract class ServiceTriggerControllerBase: AbpController
    {
        protected ServiceTriggerControllerBase()
        {
            LocalizationSourceName = ServiceTriggerConsts.LocalizationSourceName;
        }
    }
}