using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Read.Ports;

namespace RecklessSpeech.Infrastructure.Read;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddReadPorts(this IServiceCollection services)
    {
        return services
            .ConfigureRepositories();
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ISequenceQueryRepository, SequenceQueryRepository>();
    }
}