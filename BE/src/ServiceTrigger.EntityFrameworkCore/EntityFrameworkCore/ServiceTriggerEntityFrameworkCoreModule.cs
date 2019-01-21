using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ServiceTrigger.EntityFrameworkCore
{
    [DependsOn(
        typeof(ServiceTriggerCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class ServiceTriggerEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ServiceTriggerEntityFrameworkCoreModule).GetAssembly());
        }
    }
}