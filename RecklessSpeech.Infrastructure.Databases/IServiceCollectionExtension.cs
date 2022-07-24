using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Databases;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddSingleton<InMemoryRecklessSpeechDbContext>()
            .AddScoped<IDomainEventsRepository, EntityFrameworkDomainEventsRepository>();
    }
}