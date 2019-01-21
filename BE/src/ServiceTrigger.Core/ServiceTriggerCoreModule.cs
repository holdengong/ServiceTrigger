using Abp.Modules;
using Abp.Reflection.Extensions;
using ServiceTrigger.Localization;

namespace ServiceTrigger
{
    public class ServiceTriggerCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            ServiceTriggerLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ServiceTriggerCoreModule).GetAssembly());
        }
    }
}