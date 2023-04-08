namespace Ext.Shared.Web.Extensions
{
    using Middlewares;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    public static class HttpContextExtensions
    {
        public static string GetRequestId(this HttpRequest request)
        {
            var hasRequestId = request.Headers.TryGetValue(RequestIdMiddleware.RequestIdKey, out StringValues requestId);
            return hasRequestId ? requestId.ToString() : "Unknown Request Id";
        }
    }
}
