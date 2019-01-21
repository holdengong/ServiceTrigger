using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ServiceTrigger.Authorization.Roles;
using ServiceTrigger.Authorization.Users;
using ServiceTrigger.MultiTenancy;
using ServiceTrigger.Jobs;

namespace ServiceTrigger.EntityFrameworkCore
{
    public class ServiceTriggerDbContext : AbpZeroDbContext<Tenant, Role, User, ServiceTriggerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Job> Jobs { get; set; }
        public ServiceTriggerDbContext(DbContextOptions<ServiceTriggerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().ToTable("st_job");

            base.OnModelCreating(modelBuilder);
        }
    }
}
