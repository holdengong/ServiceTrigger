using Abp.Authorization;
using ServiceTrigger.Authorization.Roles;
using ServiceTrigger.Authorization.Users;

namespace ServiceTrigger.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
