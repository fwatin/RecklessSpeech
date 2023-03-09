using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Orchestration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureOrchestration(this IServiceCollection services) =>
            services.AddDispatcher()
                .AddSingleton<IDomainEventIdProvider, DomainEventIdProvider>();


        private static IServiceCollection AddDispatcher(this IServiceCollection services) =>
            services.AddTransient<IRecklessSpeechDispatcher, Dispatcher>()
                .AddTransient<Dispatcher>();
    }
}