using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ServiceTrigger.Authorization;
using ServiceTrigger.Jobs.Dtos.LTMAutoMapper;
using ServiceTrigger.Projects.Dtos.LTMAutoMapper;

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

            Configuration.Modules.AbpAutoMapper().Configurators.Add(JobMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(ProjectMapper.CreateMappings);
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
