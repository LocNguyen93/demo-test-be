namespace Ext.Shared.Web
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Middlewares;
    using System.Linq;
    using Validators;
    using System.Security.Claims;

    [ApiController]
    public class ExtBaseController : Controller
    {
        protected long UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (long.TryParse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out long id))
                        return id;
                }
                throw new System.Exception("User did not logged in.");
            }
        }

        protected string RequestId
        {
            get
            {
                if (Request.Headers.ContainsKey(RequestIdMiddleware.RequestIdKey))
                    return Request.Headers[RequestIdMiddleware.RequestIdKey];

                return "Missing Request Id";
            }
        }

        protected bool ValidateCaptcha(string captchaResponse, string recaptchaPrivateKey)
        {
            if (!string.IsNullOrEmpty(captchaResponse))
            {
                var captchaResult = ReCaptchaValidator.Verify(recaptchaPrivateKey, captchaResponse);
                if (captchaResult.Success)
                    return true;
            }

            return false;
        }

        protected new OkObjectResult Ok()
        {
            return base.Ok(ResultModel.Create());
        }

        protected new OkObjectResult Ok(object value)
        {
            return base.Ok(ResultModel.Create(value));
        }

        protected OkObjectResult InvalidModel()
        {
            var modelErrors = ModelState.Where(x => x.Value.Errors.Any()).Select(x => new { f = x.Key, err = x.Value.Errors.First().ErrorMessage });
            return base.Ok(ResultModel.Create(false, "invalid_data_provided", "Invalid data provided", modelErrors));
        }

        protected OkObjectResult ErrorMessage(string errorCode, string msg)
        {
            return base.Ok(ResultModel.Create(false, errorCode, msg));
        }

        protected OkObjectResult Error(string errorCode, string msg, object value)
        {
            return base.Ok(ResultModel.Create(false, errorCode, msg, value));
        }

        protected UnauthorizedObjectResult Unauthorized(string errorCode, string msg)
        {
            return base.Unauthorized(ResultModel.Create(false, errorCode, msg));
        }

        protected new UnauthorizedObjectResult Unauthorized()
        {
            return base.Unauthorized(ResultModel.Create(false));
        }

        protected OkObjectResult UnknownError()
        {
            return ErrorMessage("unknown_err", "Unknown error");
        }
    }
}
