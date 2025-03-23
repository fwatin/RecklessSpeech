using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Read.Queries.Notes.Services;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Anki;
using RecklessSpeech.Infrastructure.Sequences.Gateways.ChatGpt;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.Mijnwoordenboek;
using System;
using System.Net.Http;

namespace RecklessSpeech.Web
{
    public static class SequenceGatewayExtensions
    {
        public static IServiceCollection AddSequenceGateways(this IServiceCollection services) =>
            services
                .AddSequenceNoteGateway()
                .AddSequenceChatGptGateway()
                .AddSequenceTranslatorGateway();

        private static IServiceCollection AddSequenceNoteGateway(this IServiceCollection services)
        {
            services.AddOptions<AnkiSequenceSettings>()
                .BindConfiguration(AnkiSequenceSettings.SECTION_KEY)
                .ValidateDataAnnotations();

            Action<IServiceProvider,HttpClient> configureClient = (provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<AnkiSequenceSettings>>();
                string path = options.Value.Url;
                client.BaseAddress = new(path);
            };
            
            services.AddHttpClient<INoteGateway, HttpAnkiNoteGateway>(configureClient);
            services.AddHttpClient<IReadNoteGateway, HttpAnkiNoteGateway>(configureClient);

            return services;
        }

        private static IServiceCollection AddSequenceChatGptGateway(this IServiceCollection services)
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

        private static IServiceCollection AddSequenceTranslatorGateway(this IServiceCollection services)
        {
            services.AddSingleton<ITranslatorGatewayFactory, TranslatorGatewayFactory>();

            return services;
        }
    }
}