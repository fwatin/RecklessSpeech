using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Sequences.Executors
{
    public static class EventExecutorExtension
    {
        public static IServiceCollection AddEventsExecutors(this IServiceCollection services) =>
            services
                .AddScoped<IDomainEventExecutor, RepositoryExecutor>();
    }
}