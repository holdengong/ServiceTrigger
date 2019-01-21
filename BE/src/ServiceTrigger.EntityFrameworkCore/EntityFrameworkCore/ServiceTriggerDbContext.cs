using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ServiceTrigger.Authorization.Roles;
using ServiceTrigger.Authorization.Users;
using ServiceTrigger.MultiTenancy;
using ServiceTrigger.Jobs;
using ServiceTrigger.Projects;

namespace ServiceTrigger.EntityFrameworkCore
{
    public class ServiceTriggerDbContext : AbpZeroDbContext<Tenant, Role, User, ServiceTriggerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public ServiceTriggerDbContext(DbContextOptions<ServiceTriggerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().ToTable("st_job");
            modelBuilder.Entity<Project>().ToTable("st_project");

            base.OnModelCreating(modelBuilder);
        }
    }
}
