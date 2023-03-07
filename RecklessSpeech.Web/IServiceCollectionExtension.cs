using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecklessSpeech.Web.Configuration;
using RecklessSpeech.Web.Configuration.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace RecklessSpeech.Web
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddWebDependencies(this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            services
                .AddTransient<IConfigureOptions<KestrelServerOptions>, ConfigureKestrelOptions>()
                .AddMvcServices()
                .ConfigureApiVersioning()
                .AddHttpContextAccessor()
                .AddDispatcher();

            if (environment.EnvironmentName != "acceptancetest")
            {
                services.AddSwaggerServices();
            }

            return services;
        }

        private static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IConfigureOptions<JsonOptions>, ConfigureMvcOptions>();

            return services;
        }

        private static IServiceCollection AddDispatcher(this IServiceCollection services) =>
            services.AddMediatR(Assembly.GetCallingAssembly())
                .AddScoped<WebDispatcher>();

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services) =>
            services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagger>()
                .AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwagger>()
                .AddSwaggerGen(options => options.OperationFilter<JsonQueryOperationFilter>());

        public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services) =>
            services
                .AddApiVersioning(
                    setup =>
                    {
                        setup.DefaultApiVersion = new(1, 0);
                        setup.AssumeDefaultVersionWhenUnspecified = true;
                        setup.ReportApiVersions = true;
                        setup.ApiVersionReader = new UrlSegmentApiVersionReader();
                    })
                .AddVersionedApiExplorer(
                    setup =>
                    {
                        setup.GroupNameFormat = "'v'VVV";
                        setup.SubstituteApiVersionInUrl = true;
                    });
    }
}