using Abp.Authorization;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using ServiceTrigger.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceTrigger.Web.Host.Startup
{
    public class DashboardAuthorization : IDashboardAuthorizationFilter
    {
        public DashboardAuthorization()
        {
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
