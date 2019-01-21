using Abp.Application.Services;

namespace ServiceTrigger
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ServiceTriggerAppServiceBase : ApplicationService
    {
        protected ServiceTriggerAppServiceBase()
        {
            LocalizationSourceName = ServiceTriggerConsts.LocalizationSourceName;
        }
    }
}