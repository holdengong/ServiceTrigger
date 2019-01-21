using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ServiceTrigger.EntityFrameworkCore
{
    public class ServiceTriggerDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public ServiceTriggerDbContext(DbContextOptions<ServiceTriggerDbContext> options) 
            : base(options)
        {

        }
    }
}
