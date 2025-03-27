using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Questioner.Ports;
using System.Net.Http.Json;
using System.Text.Json;

namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    public class ChatGptGateway : IChatGptGateway
    {
        private readonly HttpClient client;
        private readonly IOptions<ChatGptSettings> settings;

        public ChatGptGateway(HttpClient client, IOptions<ChatGptSettings> settings)
        {
            this.client = client;
            this.settings = settings;
        }

        public async Task<IReadOnlyList<string>> GetInterests()
        {
            string question = "What are your interests?";
            
            ChatGptRequest request = new(this.settings.Value.ModelName,
                new()
                {
                    new("user", question)
                });
            
            
            HttpRequestMessage requestMessage = new(HttpMethod.Post, "chat/completions")
            {
                Content = JsonContent.Create(request)
            };
            HttpResponseMessage httpResponseMessage = await this.client.SendAsync(requestMessage);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };
            ChatGptResponse? response = await httpResponseMessage.Content.ReadFromJsonAsync<ChatGptResponse>(options);

            if (response is null) return null;

            return [""];
        }

    }
}