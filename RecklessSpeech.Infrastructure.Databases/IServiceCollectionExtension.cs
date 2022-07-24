using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Databases;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddInMemoryDbContextServices()
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IDomainEventsRepository, EntityFrameworkDomainEventsRepository>();
    }

    private static IServiceCollection AddInMemoryDbContextServices(this IServiceCollection services)
    {
        var dbContext = new InMemoryRecklessSpeechDbContext();
        
        return services
            .AddSingleton(dbContext)
            .AddSingleton<ISequencesDbContext>(new SequencesDbContext(dbContext));
    }
    
}