namespace RF.Web.Api.Controllers
{
    using Common;
    using Ext.Shared.DataAccess;
    using Ext.Shared.Web;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    public class RFBaseController : ExtBaseController
    {
        protected string UserType
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                    return User.Claims.First(x => x.Type == RfConstants.ClaimTypes.UserType).Value;
                throw new System.Exception("User did not logged in.");
            }
        }

        protected OkObjectResult Result(Result result)
        {
            if (result.Succeeded)
                return Ok();
            else
                return ErrorMessage(result.ErrorCode, result.ErrorMessage);
        }

        protected OkObjectResult Result<T>(Result<T> result)
        {
            if (result.Succeeded)
                return Ok(result.Data);
            else
                return ErrorMessage(result.ErrorCode, result.ErrorMessage);
        }

        protected OkObjectResult Result<T, K>(Result<T> result, Func<T, K> mapping)
        {
            if (result.Succeeded)
                return Ok(mapping(result.Data));
            else
                return ErrorMessage(result.ErrorCode, result.ErrorMessage);
        }
    }
}
