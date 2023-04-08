namespace Ext.Shared.Web.Middlewares
{
    using Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;

    public class ExceptionCatcherMiddleware
    {
        protected readonly RequestDelegate _next;
        protected readonly ILogger _logger;
        protected readonly bool IsDevelopment;

        public ExceptionCatcherMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, bool isDevelopment)
        {
            _next = next;
            _logger = loggerFactory == null ? throw new ArgumentNullException(nameof(loggerFactory)) : loggerFactory.CreateLogger("ExceptionCatcher");
            IsDevelopment = isDevelopment;
        }

        public async Task Invoke(HttpContext context)
        {
            await DoInvokeAsync(context);
        }

        protected virtual async Task DoInvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurs. RequestId: {0}", context.Request.GetRequestId());
                context.Response.StatusCode = 500;
                if (IsDevelopment)
                    await WriteResponse(context, ResultModel.Create(false, "exception_occurs", ex.Message + ". " + ex.StackTrace));
                else
                    await WriteResponse(context, ResultModel.Create(false, "unknown_error", "Unknown error"));
            }
        }

        protected async Task WriteResponse(HttpContext context, ResultModel response)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
