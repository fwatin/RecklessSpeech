using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Databases;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddInMemoryDbContext()
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IDomainEventsRepository, DomainEventsRepository>();
    }

    private static IServiceCollection AddInMemoryDbContext(this IServiceCollection services)
    {
        return services
            .AddSingleton<ISequencesDbContext>(new InMemorySequencesDbContext());
    }
    
}