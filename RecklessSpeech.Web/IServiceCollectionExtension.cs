using System.Reflection;
using MediatR;

namespace RecklessSpeech.Web;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddWebDependencies(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        return services.AddMvcServices()
            .AddDispatcher();
    }

    private static IServiceCollection AddMvcServices(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    private static IServiceCollection AddDispatcher(this IServiceCollection services)
    {
        return services.AddMediatR(Assembly.GetCallingAssembly())
            .AddScoped<WebDispatcher>();
    }
}