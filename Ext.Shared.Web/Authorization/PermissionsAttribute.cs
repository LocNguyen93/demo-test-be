namespace Ext.Shared.Web.Authorization
{
    using Microsoft.AspNetCore.Mvc;

    public class PermissionsAttribute : TypeFilterAttribute
    {
        public PermissionsAttribute(params string[] permissions) : base(typeof(PermissionsRequirementFilter))
        {
            Arguments = new object[] { permissions };
        }
    }
}
