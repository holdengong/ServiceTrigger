using Abp.AspNetCore.Mvc.Views;

namespace ServiceTrigger.Web.Views
{
    public abstract class ServiceTriggerRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected ServiceTriggerRazorPage()
        {
            LocalizationSourceName = ServiceTriggerConsts.LocalizationSourceName;
        }
    }
}
