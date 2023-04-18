using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Anki;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.Mijnwoordenboek;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.WordReference;

namespace RecklessSpeech.Infrastructure.Read
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReadPorts(this IServiceCollection services) =>
            services
                .AddQueryRepositories()
                .AddNoteGateway()
                .AddTranslatorGateway();

        private static IServiceCollection AddQueryRepositories(this IServiceCollection services) =>
            services
                .AddScoped<ISequenceQueryRepository, InMemorySequenceQueryRepository>();

        private static IServiceCollection AddNoteGateway(this IServiceCollection services)
        {
            services.AddOptions<HttpAnkiNoteGatewayOptions>()
                .BindConfiguration("AnkiNoteGateway")
                .ValidateDataAnnotations();

            services.AddHttpClient<INoteGateway, HttpAnkiNoteGateway>(
                (_, client) =>
//                    (provider, client) =>
                {
                    // HttpAnkiNoteGatewayOptions? options =
                    //     provider.GetRequiredService<IOptions<HttpAnkiNoteGatewayOptions>>().Value;
                    client.BaseAddress = new("http://localhost:8765/"); //todo résoudre le problème plus tard - en production pas de lecture de l'appsettings
                });

            return services;
        }

        private static IServiceCollection AddTranslatorGateway(this IServiceCollection services)
        {
            services.AddSingleton<IDutchTranslatorGateway, MijnwoordenboekOnlineGateway>();

            services.AddSingleton<IWordReferenceGatewayAccess>(new WordReferenceGatewayOnlineAccess());
            services.AddSingleton<IEnglishTranslatorGateway, WordReferenceGateway>();

            return services;
        }
    }
}