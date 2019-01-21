using Microsoft.EntityFrameworkCore;

namespace ServiceTrigger.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<ServiceTriggerDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for ServiceTriggerDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
