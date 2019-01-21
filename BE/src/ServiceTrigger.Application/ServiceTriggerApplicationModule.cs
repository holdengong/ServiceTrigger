using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ServiceTrigger
{
    [DependsOn(
        typeof(ServiceTriggerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ServiceTriggerApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ServiceTriggerApplicationModule).GetAssembly());
        }
    }
}