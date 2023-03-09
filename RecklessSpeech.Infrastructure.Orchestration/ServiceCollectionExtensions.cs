using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Orchestration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureOrchestration(this IServiceCollection services)
        {
            services.AddDispatcher();
        }


        private static void AddDispatcher(this IServiceCollection services)
        {
            services.AddTransient<IRecklessSpeechDispatcher, Dispatcher>()
                .AddTransient<Dispatcher>();
        }
    }
}