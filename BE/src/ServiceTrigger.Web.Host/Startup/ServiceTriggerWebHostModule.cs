using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ServiceTrigger.Configuration;
using Abp.Hangfire.Configuration;
using Abp.Hangfire;

namespace ServiceTrigger.Web.Host.Startup
{
    [DependsOn(
       typeof(ServiceTriggerWebCoreModule))]
    public class ServiceTriggerWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ServiceTriggerWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ServiceTriggerWebHostModule).GetAssembly());
        }
    }
}
