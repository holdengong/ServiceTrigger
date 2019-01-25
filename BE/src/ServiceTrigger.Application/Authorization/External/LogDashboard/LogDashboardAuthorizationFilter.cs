using LogDashboard;
using LogDashboard.Authorization;

namespace ServiceTrigger.Authorization.External.LogDashboard
{
    public class LogDashboardAuthorizationFilter : ILogDashboardAuthorizationFilter
    {
        private readonly string _authenticateApiUrl;
        public LogDashboardAuthorizationFilter(string authenticateApiUrl)
        {
            _authenticateApiUrl = authenticateApiUrl;
        }

        public bool Authorization(LogDashboardContext context)
        {
            return ExternalAuthorizationHelper.IsGranted(context.HttpContext, _authenticateApiUrl, PermissionNames.Tools_Log);
        }
    }
}
