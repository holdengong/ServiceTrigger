using System;
using System.Threading.Tasks;
using Abp.TestBase;
using ServiceTrigger.EntityFrameworkCore;
using ServiceTrigger.Tests.TestDatas;

namespace ServiceTrigger.Tests
{
    public class ServiceTriggerTestBase : AbpIntegratedTestBase<ServiceTriggerTestModule>
    {
        public ServiceTriggerTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<ServiceTriggerDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<ServiceTriggerDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<ServiceTriggerDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<ServiceTriggerDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<ServiceTriggerDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<ServiceTriggerDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<ServiceTriggerDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<ServiceTriggerDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
