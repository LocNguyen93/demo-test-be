namespace Ext.Shared.Web.Extensions
{
    using Models;
    using Microsoft.AspNetCore.Builder;
    using Middlewares;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIdMiddleware>();
        }

        public static IApplicationBuilder UseExceptionCatcher(this IApplicationBuilder builder, bool isDevelopment)
        {
            return builder.UseMiddleware<ExceptionCatcherMiddleware>(isDevelopment);
        }

        public static IApplicationBuilder UseRequireApiKey(this IApplicationBuilder builder, string header, string value)
        {
            return builder.UseMiddleware<RequireApiKeyMiddleware>(new ApiKeyOptions { ApiKeyHeader = header, ApiKeyValue = value });
        }
    }
}
