using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Orchestration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureOrchestration(this IServiceCollection services)
    {
        return services.AddDispatcher()
            .AddSingleton<IDomainEventIdProvider, DomainEventIdProvider>();
    }


    private static IServiceCollection AddDispatcher(this IServiceCollection services)
    {
        return services.AddTransient<IRecklessSpeechDispatcher, Dispatcher>()
            .AddTransient<Dispatcher>();
    }
}