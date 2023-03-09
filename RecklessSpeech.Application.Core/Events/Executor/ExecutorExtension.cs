using Microsoft.Extensions.DependencyInjection;

namespace RecklessSpeech.Application.Core.Events.Executor
{
    public static class ExecutorExtension
    {
        public static IServiceCollection AddEventsExecutor(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsExecutorManager, DomainEventsExecutorManager>();
            return services;
        }
    }
}