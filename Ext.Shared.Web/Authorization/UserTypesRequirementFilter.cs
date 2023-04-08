namespace Ext.Shared.Web.Authorization
{
    using Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class UserTypesRequirementFilter : IAuthorizationFilter
    {
        private readonly string[] types;

        public UserTypesRequirementFilter(string[] types)
        {
            this.types = types;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.IsInType(types))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
