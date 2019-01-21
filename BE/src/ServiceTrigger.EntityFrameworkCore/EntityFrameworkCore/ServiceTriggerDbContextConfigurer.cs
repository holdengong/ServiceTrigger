using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ServiceTrigger.EntityFrameworkCore
{
    public static class ServiceTriggerDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ServiceTriggerDbContext> builder, string connectionString)
        {
            // builder.UseSqlServer(connectionString);

            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ServiceTriggerDbContext> builder, DbConnection connection)
        {
            // builder.UseSqlServer(connection);

            builder.UseMySql(connection);
        }
    }
}
