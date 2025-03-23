using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Questioner.Ports;
using RecklessSpeech.Infrastructure.Questioner;
using System;

namespace RecklessSpeech.Web
{
    public static class QuestionerGatewayExtensions
    {
        public static IServiceCollection AddQuestionerGateways(this IServiceCollection services) =>
            services
                .AddQuestionerNoteGateway();

        private static IServiceCollection AddQuestionerNoteGateway(this IServiceCollection services)
        {
            services.AddOptions<AnkiQuestionerSettings>()
                .BindConfiguration(AnkiQuestionerSettings.SECTION_KEY)
                .ValidateDataAnnotations();

            services
                .AddHttpClient<IReadNoteGateway, HttpAnkiNoteGateway>(
                    (provider, client) =>
                    {
                        var options = provider.GetRequiredService<IOptions<AnkiQuestionerSettings>>();
                        client.BaseAddress = new Uri(options.Value.Url);
                    });

            return services;
        }
    }
}