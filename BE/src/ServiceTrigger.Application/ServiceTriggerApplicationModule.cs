using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ServiceTrigger.Authorization;

namespace ServiceTrigger
{
    [DependsOn(
        typeof(ServiceTriggerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ServiceTriggerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ServiceTriggerAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ServiceTriggerApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
