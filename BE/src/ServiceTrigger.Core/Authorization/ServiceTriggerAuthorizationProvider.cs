using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ServiceTrigger.Authorization
{
    public class ServiceTriggerAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Projects_View, L("Projects_View"));
            context.CreatePermission(PermissionNames.Projects_Save, L("Projects_Save"));
            context.CreatePermission(PermissionNames.Projects_Delete, L("Projects_Delete"));

            context.CreatePermission(PermissionNames.Jobs_View, L("Jobs_View"));
            context.CreatePermission(PermissionNames.Jobs_Save, L("Jobs_Save"));
            context.CreatePermission(PermissionNames.Jobs_Delete, L("Jobs_Delete"));
            context.CreatePermission(PermissionNames.Jobs_Trigger, L("Jobs_Trigger"));
            context.CreatePermission(PermissionNames.Jobs_Enable, L("Jobs_Enable"));
            context.CreatePermission(PermissionNames.Jobs_Disable, L("Jobs_Disable"));

            context.CreatePermission(PermissionNames.Tools_HangfireDashboard,L("HangfireDashboard"));

            context.CreatePermission(PermissionNames.Tools_Log, L("Log"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ServiceTriggerConsts.LocalizationSourceName);
        }
    }
}
