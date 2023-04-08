namespace RF.Web.Api
{
    using Amazon;
    using Amazon.Runtime;
    using Amazon.S3;
    using Amazon.SecurityToken;
    using AppSettings;
    using AutoMapper;
    using Entities;
    using Ext.Shared.Web.Extensions;
    using Ext.Shared.Web.ModelBinders;
    using Ext.Shared.Web.Models;
    using Ext.Shared.Web.Validators;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using RF.Web.Api.Extensions;
    using Services;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAppSettings(Configuration)
                .AddAutoMapper(typeof(Startup))
                .ConfigureWebApp()
                .AddServices(Configuration)
                .AddDataAccess()
                .AddSwagger(bool.Parse(Configuration["Application:EnableSwagger"]), Configuration);

            services.AddControllers();
            services.AddOptions();
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            if (bool.Parse(Configuration["Application:EnableSwagger"]))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(Configuration["Application:SwaggerApiBaseUrl"] + "/swagger/product/swagger.json", "Product API v1");
                    c.SwaggerEndpoint(Configuration["Application:SwaggerApiBaseUrl"] + "/swagger/shop/swagger.json", "Shop API v1");
                    c.SwaggerEndpoint(Configuration["Application:SwaggerApiBaseUrl"] + "/swagger/customer/swagger.json", "Customer API v1");
                    c.SwaggerEndpoint(Configuration["Application:SwaggerApiBaseUrl"] + "/swagger/order/swagger.json", "Order API v1");
                });
            }

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration.GetSection("Application:AllowedOrigins").Get<string[]>())
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });

            app.UseRequestId();
            app.UseExceptionCatcher(env.IsDevelopment());

            var internalApi = Configuration.GetSection("Authentication:InternalApi").Get<InternalApi>();
            app.UseWhen(
                x => internalApi.ApiKeyPath.Any(y => x.Request.Path.StartsWithSegments(y)),
                x => x.UseRequireApiKey(internalApi.ApiKeyHeader, internalApi.ApiKeyValue)
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
