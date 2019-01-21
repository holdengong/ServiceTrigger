using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ServiceTrigger.Web.Startup;
namespace ServiceTrigger.Web.Tests
{
    [DependsOn(
        typeof(ServiceTriggerWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class ServiceTriggerWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ServiceTriggerWebTestModule).GetAssembly());
        }
    }
}