namespace Ext.Shared.Web.Authorization
{
    using Microsoft.AspNetCore.Mvc;

    public class UserTypesAttribute : TypeFilterAttribute
    {
        public UserTypesAttribute(params string[] types) : base(typeof(UserTypesRequirementFilter))
        {
            Arguments = new object[] { types };
        }
    }
}
