using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Anki;
using RecklessSpeech.Infrastructure.Sequences.Gateways.ChatGpt;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.Mijnwoordenboek;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.WordReference;
using System;

namespace RecklessSpeech.Web
{
    public static class GatewayExtensions
    {
        public static IServiceCollection AddGateways(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddNoteGateway(configuration)
                .AddChatGptGateway(configuration)
                .AddTranslatorGateway();

        private static IServiceCollection AddNoteGateway(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<HttpAnkiNoteGatewayOptions>()
                .BindConfiguration("AnkiNoteGateway")
                .ValidateDataAnnotations();

            services.AddHttpClient<INoteGateway, HttpAnkiNoteGateway>(
                (_, client) =>
                {
                    string path = configuration["AnkiPath"];
                    client.BaseAddress = new(path);
                });

            return services;
        }

        private static IServiceCollection AddChatGptGateway(this IServiceCollection services,
            IConfiguration configuration)
        {
            string chatGptKey = configuration["CHATGPT_KEY"];
            services.AddHttpClient<IChatGptGateway, ChatGptGateway>(
                (_, client) =>
                {
                    client.BaseAddress = new Uri("https://api.openai.com/v1/");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {chatGptKey}");
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