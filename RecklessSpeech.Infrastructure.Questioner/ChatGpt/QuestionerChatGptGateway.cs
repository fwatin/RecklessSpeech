using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Questioner.Ports;
using RecklessSpeech.Domain.Questioner;
using System.Net.Http.Json;
using System.Text;
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

        public async Task<IReadOnlyList<string>> GetInterests(
            IReadOnlyCollection<Note> relatedNotes,
            Completion commandCompletion, string subject)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "ChatGpt", "templates", "answer-structure.json");
            string functionJson = await File.ReadAllTextAsync(path);

            var functionDefinition = JsonSerializer.Deserialize<FunctionDefinition>(functionJson);
            var functions = new[] { functionDefinition };

            var messages = new[]
            {
                new { role = "system", content = "Tu es un assistant qui génère des cartes Anki (Q/R)." },
                new
                {
                    role = "user",
                    content = QuestionProvider.CreateQuestionFunctionCall(subject, commandCompletion)
                }
            };

            // 4) Construire le corps de la requête
            var requestBody = new
            {
                model = this.settings.Value.ModelName,
                messages,
                functions,
                // On force l'appel de la fonction "create_cards"
                function_call = new { name = "create_cards" }
            };

            // 5) Sérialiser en JSON
            string jsonRequest = JsonSerializer.Serialize(requestBody);

            // 6) Envoyer la requête
            using var request = new HttpRequestMessage(HttpMethod.Post, "chat/completions");
            request.Headers.Add("Authorization", $"Bearer {this.settings.Value.SubscriptionKey}");
            request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.client.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();

            // 7) Désérialiser la réponse
            ChatResponse? chatResponse = null;
            try
            {
                chatResponse = JsonSerializer.Deserialize<ChatResponse>(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la désérialisation de la réponse : " + ex.Message);
                Console.WriteLine("Contenu : " + responseContent);
            }

            if (chatResponse?.Choices == null || chatResponse.Choices.Count == 0)
            {
                Console.WriteLine("Pas de réponse du modèle.");
                return Array.Empty<string>();
            }

            // 8) Récupérer la function_call
            var functionCall = chatResponse.Choices[0].Message.FunctionCall;

            Console.WriteLine("Nom de la fonction appelée : " + functionCall.Name);
            Console.WriteLine("Arguments JSON (brut) : " + functionCall.Arguments);

            // 9) Parser les arguments (liste de cartes)
            CreateCardsArgs? createCardsArgs = null;
            try
            {
                createCardsArgs = JsonSerializer.Deserialize<CreateCardsArgs>(functionCall.Arguments);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de parser les arguments de la fonction : " + ex.Message);
                return Array.Empty<string>();
            }

            // 10) Affichage
            if (createCardsArgs?.Cards != null)
            {
                Console.WriteLine("\n=== CARTES GÉNÉRÉES ===");
                int i = 1;
                List<string> questions = new();
                foreach (var card in createCardsArgs.Cards)
                {
                    Console.WriteLine($"{i++}. Question: {card.Question}");
                    Console.WriteLine($"   Réponse: {card.Answer}");
                    questions.Add($"{card.Question} {card.Answer}");
                }
                return questions;
            }
            Console.WriteLine("Aucune carte trouvée dans les arguments.");
            return Array.Empty<string>();
        }
    }
}