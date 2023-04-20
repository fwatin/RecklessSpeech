using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Domain.Sequences.Explanations;
using System.Net.Http.Json;
using System.Text.Json;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.ChatGpt
{
    public class ChatGptGateway : IChatGptGateway
    {
        private readonly HttpClient client;

        public ChatGptGateway(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Explanation> GetExplanation(string word, string sentence, Language language)
        {
            ChatGptRequest request = new("gpt-3.5-turbo",
                new()
                {
                    new("user", $"Peux-tu expliquer le sens du mot {language.GetLanguageInFrench()} {word} dans la phrase {sentence}")
                });

            HttpRequestMessage requestMessage = new(HttpMethod.Post, "chat/completions")
            {
                Content = JsonContent.Create(request)
            };
            HttpResponseMessage httpResponseMessage = await this.client.SendAsync(requestMessage);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            ChatGptResponse? response = await httpResponseMessage.Content.ReadFromJsonAsync<ChatGptResponse>(options);

            if (response is null) throw new("unexpected ChatGpt response is null");
            
            string path = Path.Join(AppContext.BaseDirectory, "Gateways","ChatGpt","templates", "display_explanation.html");
            string template = await File.ReadAllTextAsync(path);

            string content = template
                .Replace("{{word}}", word)
                .Replace("{{sentence}}", sentence)
                .Replace("{{language}}", language.GetLanguageInFrench())
                .Replace("{{explanation}}", response.choices.First().Message.Content);

            return Explanation.Create(Guid.NewGuid(),content, word, "ChatGpt");
        }
    }
}