namespace Ext.Shared.Web.Middlewares
{
    using Shared.Extensions;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public class RequestIdMiddleware
    {
        internal const string RequestIdKey = "Ext.RequestId";

        private readonly RequestDelegate next;

        public RequestIdMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey(RequestIdKey))
                context.Request.Headers.Add(RequestIdKey, $"{DateTime.UtcNow.ToUnixTimestampMilliseconds()}_{Guid.NewGuid()}");

            await next(context);
        }
    }
}
