using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Write.Sequences.Ports;
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
        services.AddScoped<IDomainEventsRepository, DomainEventsRepository>();

        services.AddScoped<ISequenceRepository, InMemorySequenceRepository>();
        services.AddScoped<InMemorySequenceRepository>();

        return services;
    }

    private static IServiceCollection AddInMemoryDbContext(this IServiceCollection services)
    {
        InMemorySequencesDbContext inMemorySequencesDbContext = new();

        return services
            .AddSingleton<ISequencesDbContext>(inMemorySequencesDbContext)
            .AddSingleton(inMemorySequencesDbContext);
    }
}