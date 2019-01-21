using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ServiceTrigger.Configuration;
using ServiceTrigger.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ServiceTrigger.Web.Startup
{
    [DependsOn(
        typeof(ServiceTriggerApplicationModule), 
        typeof(ServiceTriggerEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class ServiceTriggerWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public ServiceTriggerWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(ServiceTriggerConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<ServiceTriggerNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(ServiceTriggerApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ServiceTriggerWebModule).GetAssembly());
        }
    }
}