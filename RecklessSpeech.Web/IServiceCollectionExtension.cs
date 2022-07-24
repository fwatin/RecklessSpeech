namespace RecklessSpeech.Web;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddWebDependencies(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        return services.AddMvcServices();
    }
    
    public static IServiceCollection AddMvcServices(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}