namespace Ext.Shared.Web.Authorization
{
    using Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class PermissionsRequirementFilter : IAuthorizationFilter
    {
        private readonly string[] permissions;

        public PermissionsRequirementFilter(string[] permissions)
        {
            this.permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.HasPermission(permissions))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
