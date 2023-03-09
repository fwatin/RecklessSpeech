using Microsoft.Extensions.DependencyInjection;

namespace RecklessSpeech.Application.Core.Dispatch
{
    public static class DispatcherExtensions
    {
        public static void AddInfrastructureOrchestration(this IServiceCollection services) => services.AddDispatcher();


        private static void AddDispatcher(this IServiceCollection services) =>
            services.AddTransient<IDispatcher, Dispatcher>()
                .AddTransient<Dispatcher>();
    }
}