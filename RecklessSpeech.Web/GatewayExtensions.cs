using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Italian;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Anki;
using RecklessSpeech.Infrastructure.Sequences.Gateways.ChatGpt;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.Mijnwoordenboek;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.WordReference;
using System;

namespace RecklessSpeech.Web
{
    public static class GatewayExtensions
    {
        public static IServiceCollection AddGateways(this IServiceCollection services) =>
            services
                .AddNoteGateway()
                .AddChatGptGateway()
                .AddTranslatorGateway();

        private static IServiceCollection AddNoteGateway(this IServiceCollection services)
        {
            services.AddOptions<AnkiSettings>()
                .BindConfiguration(AnkiSettings.SECTION_KEY)
                .ValidateDataAnnotations();

            services.AddHttpClient<INoteGateway, HttpAnkiNoteGateway>(
                (provider, client) =>
                {
                    var options = provider.GetRequiredService<IOptions<AnkiSettings>>();
                    string path = options.Value.Url;
                    client.BaseAddress = new(path);
                });

            return services;
        }

        private static IServiceCollection AddChatGptGateway(this IServiceCollection services)
        {
            services.AddOptions<ChatGptSettings>()
                .BindConfiguration(ChatGptSettings.SECTION_KEY)
                .ValidateDataAnnotations();

            services.AddHttpClient<IChatGptGateway, ChatGptGateway>(
                (provider, client) =>
                {
                    var options = provider.GetRequiredService<IOptions<ChatGptSettings>>();
                    client.BaseAddress = new Uri(options.Value.Url);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Value.SubscriptionKey}");
                });

            return services;
        }

        private static IServiceCollection AddTranslatorGateway(this IServiceCollection services)
        {
            services.AddSingleton<IDutchTranslatorGateway, DutchMijnWoordenboekGateway>();
            services.AddSingleton<IEnglishTranslatorGateway, EnglishWordReferenceGateway>();
            services.AddSingleton<IItalianTranslatorGateway, ItalianWordReferenceGateway>();

            return services;
        }
    }
}