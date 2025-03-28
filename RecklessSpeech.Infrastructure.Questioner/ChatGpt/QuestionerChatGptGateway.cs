using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Questioner.Ports;
using RecklessSpeech.Domain.Questioner;
using System.Net.Http.Json;
using System.Text.Json;

namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    public class QuestionerChatGptGateway : IQuestionerChatGptGateway
    {
        private readonly HttpClient client;
        private readonly IOptions<ChatGptSettings> settings;

        public QuestionerChatGptGateway(HttpClient client, IOptions<ChatGptSettings> settings)
        {
            this.client = client;
            this.settings = settings;
        }

        public async Task<IReadOnlyList<string>> GetInterests(IReadOnlyCollection<Note> relatedNotes,
            Completion commandCompletion)
        {
            string question = CreateQuestion(relatedNotes, commandCompletion);

            ChatGptRequest request = new(this.settings.Value.ModelName, [new("user", question)]);


            HttpRequestMessage requestMessage = new(HttpMethod.Post, "chat/completions")
            {
                Content = JsonContent.Create(request)
            };
            HttpResponseMessage httpResponseMessage = await this.client.SendAsync(requestMessage);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };
            ChatResponse? response = await httpResponseMessage.Content.ReadFromJsonAsync<ChatResponse>(options);

            if (response is null) return null;

            return [""];
        }

        private static string CreateQuestion(IReadOnlyCollection<Note> relatedNotes, Completion commandCompletion) =>
            "Dans le texte suivant, peux tu écrire une liste de question/réponse " +
            "qui seraient des candidats pour des fiches Anki ?" +
            "Génère un ensemble de questions/réponses en format JSON :" +
            "{\n  \"cards\": [\n    {\n      \"question\": \"…\",\n      \"answer\": \"…\"\n    },\n    …\n  ]\n}\n" +
            "N’inclus aucun texte hors de la structure JSON." +
            "---" +
            "Le texte est le suivant : " + commandCompletion.Value +
            "---" +
            "Voici des fiches extraites de Anki déjà existantes pour que tu puisses t'en inspirer " +
            "mais je ne les veux pas dans ma réponse car elles sont déjà dans mon Anki." +
            string.Join("\n", relatedNotes.Select(n => n.Slimmed))+
            "---";
    }
}