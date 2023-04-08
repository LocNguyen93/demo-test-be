namespace Ext.Shared.Web.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using Models;

    public class RequireApiKeyMiddleware
    {
        protected readonly RequestDelegate next;
        protected readonly ILogger logger;
        protected readonly ApiKeyOptions options;

        public RequireApiKeyMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, ApiKeyOptions options)
        {
            this.next = next;
            this.logger = loggerFactory == null ? throw new ArgumentNullException(nameof(loggerFactory)) : loggerFactory.CreateLogger("RequireApiKeyMiddleware");
            this.options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            await DoInvokeAsync(context);
        }

        protected virtual async Task DoInvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(options.ApiKeyHeader))
            {
                var providedKey = context.Request.Headers[options.ApiKeyHeader];
                if (options.ApiKeyValue == providedKey)
                    await next.Invoke(context);
                else
                    await WriteResponse(context, ResultModel.Create(false, "unauthorized", "Unauthorized"));
            }
            else
            {
                await WriteResponse(context, ResultModel.Create(false, "unauthorized", "Unauthorized"));
            }
        }

        protected async Task WriteResponse(HttpContext context, ResultModel response)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
