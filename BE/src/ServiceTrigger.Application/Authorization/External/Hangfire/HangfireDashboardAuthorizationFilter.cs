using Hangfire.Dashboard;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ServiceTrigger.Authorization.External.Hangfire
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string _authenticateApiUrl;

        public HangfireDashboardAuthorizationFilter(string authenticateApiUrl)
        {
            _authenticateApiUrl = authenticateApiUrl;
        }

        /// <summary>
        /// hangfire dashboard授权
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize(DashboardContext context)
        {
            return ExternalAuthorizationHelper.IsGranted(context.GetHttpContext(), _authenticateApiUrl, PermissionNames.Tools_HangfireDashboard);
        }

    }
}
