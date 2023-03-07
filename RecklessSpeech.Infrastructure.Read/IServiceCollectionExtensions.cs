using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;
using RecklessSpeech.Infrastructure.Sequences.AnkiGateway;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.WordReference;

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
                .AddScoped<ISequenceQueryRepository, InMemorySequenceQueryRepository>()
                .AddScoped<ILanguageDictionaryQueryRepository, InMemoryLanguageDictionaryQueryRepository>();

        private static IServiceCollection AddNoteGateway(this IServiceCollection services)
        {
            services.AddOptions<HttpAnkiNoteGatewayOptions>()
                .BindConfiguration("AnkiNoteGateway")
                .ValidateDataAnnotations();

            services.AddHttpClient<INoteGateway, HttpAnkiNoteGateway>(
                (provider, client) =>
                {
                    HttpAnkiNoteGatewayOptions? options =
                        provider.GetRequiredService<IOptions<HttpAnkiNoteGatewayOptions>>().Value;
                    client.BaseAddress = new(options.Path);
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
}