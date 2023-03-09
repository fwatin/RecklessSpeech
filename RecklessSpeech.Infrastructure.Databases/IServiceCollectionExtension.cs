using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Core.Events.Executor;

namespace RecklessSpeech.Infrastructure.Databases
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEventsExecutor(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsExecutorManager, DomainEventsExecutorManager>();
            return services;
        }

       
    }
}