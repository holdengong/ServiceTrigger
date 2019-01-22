using Abp.AutoMapper;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ServiceTrigger.Authorization;
using ServiceTrigger.Jobs.Dtos.LTMAutoMapper;
using ServiceTrigger.Projects.Dtos.LTMAutoMapper;
using Abp.Hangfire;

namespace ServiceTrigger
{
    [DependsOn(
        typeof(ServiceTriggerCoreModule), 
        typeof(AbpHangfireAspNetCoreModule),
        typeof(AbpAutoMapperModule))]
    public class ServiceTriggerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ServiceTriggerAuthorizationProvider>();

            Configuration.Modules.AbpAutoMapper().Configurators.Add(JobMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(ProjectMapper.CreateMappings);

            Configuration.BackgroundJobs.UseHangfire();
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
