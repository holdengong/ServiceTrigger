using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ServiceTrigger.Configuration;
using ServiceTrigger.Web;

namespace ServiceTrigger.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ServiceTriggerDbContextFactory : IDesignTimeDbContextFactory<ServiceTriggerDbContext>
    {
        public ServiceTriggerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ServiceTriggerDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ServiceTriggerDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ServiceTriggerConsts.ConnectionStringName));

            return new ServiceTriggerDbContext(builder.Options);
        }
    }
}
