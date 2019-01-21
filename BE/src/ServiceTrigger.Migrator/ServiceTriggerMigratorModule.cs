using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ServiceTrigger.Configuration;
using ServiceTrigger.EntityFrameworkCore;
using ServiceTrigger.Migrator.DependencyInjection;

namespace ServiceTrigger.Migrator
{
    [DependsOn(typeof(ServiceTriggerEntityFrameworkModule))]
    public class ServiceTriggerMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public ServiceTriggerMigratorModule(ServiceTriggerEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(ServiceTriggerMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                ServiceTriggerConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ServiceTriggerMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
