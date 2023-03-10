using Microsoft.Extensions.DependencyInjection;

namespace RecklessSpeech.Web.Configuration.Cors;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsService(this IServiceCollection services)
    {
        //services
        services.AddCors(options =>
        {
            options.AddPolicy("AllowEverything",
                builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
        });
        return services;
    }
}