using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Infrastructure.Sequences.AnkiGateway;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.WordReference;

namespace RecklessSpeech.Infrastructure.Read;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReadPorts(this IServiceCollection services)
    {
        return services
                .AddQueryRepositories()
                .AddNoteGateway()
                .AddTranslatorGateway()
            ;
    }

    private static IServiceCollection AddQueryRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ISequenceQueryRepository, InMemorySequenceQueryRepository>()
            .AddScoped<ILanguageDictionaryQueryRepository, InMemoryLanguageDictionaryQueryRepository>();
    }

    private static IServiceCollection AddNoteGateway(this IServiceCollection services)
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

    private static IServiceCollection AddTranslatorGateway(this IServiceCollection services)
    {
        services.AddSingleton<IMijnwoordenboekGatewayAccess>(new MijnwoordenboekGatewayOnlineAccess());
        services.AddSingleton<IDutchTranslatorGateway, MijnwoordenboekGateway>();

        services.AddSingleton<IWordReferenceGatewayAccess>(new WordReferenceGatewayOnlineAccess());
        services.AddSingleton<IEnglishTranslatorGateway, WordReferenceGateway>();

        return services;
    }
}