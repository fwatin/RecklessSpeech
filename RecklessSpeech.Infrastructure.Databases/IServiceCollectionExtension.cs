using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Databases;

public static class ServiceCollectionExtension
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

        services.AddScoped<IExplanationRepository, InMemoryExplanationRepository>();
        services.AddScoped<InMemoryExplanationRepository>();

        
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