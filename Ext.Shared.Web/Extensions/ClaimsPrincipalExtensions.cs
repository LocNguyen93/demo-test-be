namespace Ext.Shared.Web.Extensions
{
    using RF.Common;
    using System;
    using System.Linq;
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal claimPrincipal, params string[] permissions)
        {
            if (claimPrincipal.IsSystemAdmin())
                return true;

            var permissionClaim = claimPrincipal.FindFirst(x => x.Type == ExtConstants.ClaimTypes.Permissions);
            if (permissionClaim != null)
            {
                var pers = permissionClaim.Value.Split(',');
                return pers.Intersect(permissions).Any();
            }

            return false;
        }

        public static string HasPermissionStr(this ClaimsPrincipal claimPrincipal, params string[] permissions)
        {
            return HasPermission(claimPrincipal, permissions).ToString().ToLower();
        }

        public static bool IsSystemAdmin(this ClaimsPrincipal claimPrincipal)
        {
            return claimPrincipal.IsInRole(ExtConstants.Roles.SystemAdmin);
        }

        public static bool IsInType(this ClaimsPrincipal claimsPrincipal, params string[] types)
        {
            var userTypeClaim = claimsPrincipal.FindFirst(x => x.Type == RfConstants.ClaimTypes.UserType);
            if (userTypeClaim != null)
                return types.Contains(userTypeClaim.Value, StringComparer.OrdinalIgnoreCase);

            return false;
        }
    }
}
