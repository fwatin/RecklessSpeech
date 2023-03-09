using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Sequences
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSequencePorts(this IServiceCollection services) =>
            services
                .AddScoped<IDomainEventExecutor, SequenceDomainEventRepository>();
    }
}