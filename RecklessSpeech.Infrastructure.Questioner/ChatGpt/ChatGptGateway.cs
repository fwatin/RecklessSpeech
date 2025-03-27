using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Questioner.Ports;
using RecklessSpeech.Domain.Questioner;
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

        public async Task<IReadOnlyList<string>> GetInterests(IReadOnlyCollection<Note> relatedNotes,
            Completion commandCompletion)
        {
            string question = "Dans le texte suivant, peux tu écrire une liste de questions " +
                              "qui seraient des candidats pour des fiches Anki ?" +
                              "en retour je souhaite une liste." +
                              "---" +
                              "Le texte est le suivant : " + commandCompletion +
                              "---" +
                              "Voici des extraits de fiches pour que tu puisses t'inspirer du niveau de détail et de mon style : " +
                              string.Join("\n", relatedNotes.Select(n => n.Slimmed));

            ChatGptRequest request = new(this.settings.Value.ModelName,
                new() { new("user", question) });


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