using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SecurityToken;
using Ext.Shared.Web.ModelBinders;
using Ext.Shared.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RF.Web.Api.AppSettings;
using RF.Web.Api.DataAccess;
using RF.Web.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RF.Web.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"))
            ;

            return services;
        }

        public static IServiceCollection AddAwsServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var awsOptions = Configuration.GetAWSOptions();
            awsOptions.Region = RegionEndpoint.GetBySystemName(Configuration["Aws:Region"]);
            awsOptions.Credentials = new BasicAWSCredentials(Configuration["Aws:Credential:AccessKey"], Configuration["Aws:Credential:SecretKey"]);
            awsOptions.DefaultClientConfig.MaxErrorRetry = 5;
            awsOptions.DefaultClientConfig.Timeout = TimeSpan.FromSeconds(900);
            //awsOptions.DefaultClientConfig.ReadWriteTimeout = TimeSpan.FromSeconds(900);

            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonS3>();
            services.AddAWSService<IAmazonSecurityTokenService>();

            return services;
        }

        public static IServiceCollection ConfigureWebApp(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var modelErrors = context.ModelState.Where(x => x.Value.Errors.Any()).Select(x => new { f = x.Key, err = x.Value.Errors.First().ErrorMessage });
                    return new OkObjectResult(ResultModel.Create(false, "invalid_data_provided", "Invalid data provided", modelErrors));
                };
            });

            services.AddResponseCaching();
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new JObjectBinderProvider());
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, bool enable, IConfiguration configuration)
        {
            if (enable)
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("product", new OpenApiInfo { Title = "Product API", Version = "v1" });
                    c.SwaggerDoc("shop", new OpenApiInfo { Title = "Shop API", Version = "v1" });
                    c.SwaggerDoc("customer", new OpenApiInfo { Title = "Customer API", Version = "v1" });
                    c.SwaggerDoc("order", new OpenApiInfo { Title = "Order API", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Please enter JWT Token", Name = "Authorization", Type = SecuritySchemeType.ApiKey });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    });

                    //c.DocumentFilter<MyDocumentFilter>(configuration["Application:SwaggerApiBaseUrl"]);
                    //c.OperationFilter<FileUploadOperation>();
                }).AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }

        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IProductDA, ProductDA>();
            services.AddScoped<IShopDA, ShopDA>();
            services.AddScoped<ICustomerDA, CustomerDA>();
            services.AddScoped<IOrderDA, OrderDA>();
            return services;
        }
    }
}
