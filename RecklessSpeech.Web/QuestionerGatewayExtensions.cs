using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Questioner.Ports;
using RecklessSpeech.Infrastructure.Questioner;
using RecklessSpeech.Infrastructure.Questioner.ChatGpt;
using System;

namespace RecklessSpeech.Web
{
    public static class QuestionerGatewayExtensions
    {
        public static IServiceCollection AddQuestionerGateways(this IServiceCollection services) =>
            services
                .AddQuestionerNoteGateway()
                .AddQuestionerChatGptGateway();

        private static IServiceCollection AddQuestionerNoteGateway(this IServiceCollection services)
        {
            services.AddOptions<AnkiQuestionerSettings>()
                .BindConfiguration(AnkiQuestionerSettings.SECTION_KEY)
                .ValidateDataAnnotations();

            services
                .AddHttpClient<IQuestionerReadNoteGateway, HttpAnkiNoteGateway>(
                    (provider, client) =>
                    {
                        var options = provider.GetRequiredService<IOptions<AnkiQuestionerSettings>>();
                        client.BaseAddress = new Uri(options.Value.Url);
                    });

            return services;
        }
        
        private static IServiceCollection AddQuestionerChatGptGateway(this IServiceCollection services)
        {
            services.AddOptions<ChatGptSettings>()
                .BindConfiguration(ChatGptSettings.SECTION_KEY)
                .ValidateDataAnnotations();

            services.AddHttpClient<IQuestionerChatGptGateway, QuestionerChatGptGateway>(
                (provider, client) =>
                {
                    var options = provider.GetRequiredService<IOptions<ChatGptSettings>>();
                    client.BaseAddress = new Uri(options.Value.Url);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Value.SubscriptionKey}");
                });

            return services;
        }
    }
}