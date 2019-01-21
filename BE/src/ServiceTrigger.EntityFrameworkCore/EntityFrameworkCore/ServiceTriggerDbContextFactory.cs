using ServiceTrigger.Configuration;
using ServiceTrigger.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ServiceTrigger.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class ServiceTriggerDbContextFactory : IDesignTimeDbContextFactory<ServiceTriggerDbContext>
    {
        public ServiceTriggerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ServiceTriggerDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(ServiceTriggerConsts.ConnectionStringName)
            );

            return new ServiceTriggerDbContext(builder.Options);
        }
    }
}