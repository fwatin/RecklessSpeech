using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Infrastructure.Sequences.AnkiGateway;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;

namespace RecklessSpeech.Infrastructure.Read;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddReadPorts(this IServiceCollection services)
    {
        return services
                .AddRepositories()
                .AddNoteGateway()
                .AddTranslatorGateway()
            ;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ISequenceQueryRepository, InMemorySequenceQueryRepository>();
    }

    public static IServiceCollection AddNoteGateway(this IServiceCollection services)
    {
        services.AddOptions<HttpAnkiNoteGatewayOptions>()
            .BindConfiguration("AnkiNoteGateway")
            .ValidateDataAnnotations();

        services.AddHttpClient<INoteGateway, HttpAnkiNoteGateway>(
            (provider, client) =>
            {
                HttpAnkiNoteGatewayOptions? options = provider.GetRequiredService<IOptions<HttpAnkiNoteGatewayOptions>>().Value;
                client.BaseAddress = new Uri(options.Path);
            });

        return services;
    }

    public static IServiceCollection AddTranslatorGateway(this IServiceCollection services)
    {
        services.AddSingleton<IMijnwoordenboekGatewayAccess>(new MijnwoordenboekGatewayLocalAccess());
        services.AddSingleton<ITranslatorGateway, MijnwoordenboekGateway>();
        return services;
    }
}