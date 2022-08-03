using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Infrastructure.Sequences.AnkiGateway;

namespace RecklessSpeech.Infrastructure.Read;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddReadPorts(this IServiceCollection services)
    {
        return services
            .ConfigureRepositories()
            .ConfigureNoteGateway()
            ;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ISequenceQueryRepository, InMemorySequenceQueryRepository>();
    }
    
    public static IServiceCollection ConfigureNoteGateway(this IServiceCollection services)
    {
        services.AddHttpClient<INoteGateway, HttpAnkiNoteGateway>();
        return services;
    }
}