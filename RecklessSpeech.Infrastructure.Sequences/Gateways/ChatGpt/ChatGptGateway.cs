using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using System.Net.Http.Json;
using System.Text.Json;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.ChatGpt
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

        public async Task<Explanation> GetExplanation(Sequence sequence)
        {
            var sentences = sequence.OriginalSentences;
            ChatGptRequest request = new(this.settings.Value.ModelName,
                new()
                {
                    new("user", sequence.SentenceToAskChatGptExplanation +
                                $"sachant que cette phrase fait partie du groupe de phrase suivant {sequence.OriginalSentences!.Joined()}")
                });
            HttpRequestMessage requestMessage = new(HttpMethod.Post, "chat/completions")
            {
                Content = JsonContent.Create(request)
            };
            HttpResponseMessage httpResponseMessage = await this.client.SendAsync(requestMessage);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };
            ChatGptResponse? response = await httpResponseMessage.Content.ReadFromJsonAsync<ChatGptResponse>(options);

            if (response is null) return null;

            string path = Path.Join(AppContext.BaseDirectory, "Gateways", "ChatGpt", "templates",
                "display_explanation.html");
            string template = await File.ReadAllTextAsync(path);

            string chatGptResponse = GetContent(response);

            string htmlContent = template
                .Replace("{{question-for-explanation}}", sequence.SentenceToAskChatGptExplanation)
                .Replace("{{central}}", sentences.GetCentralSentence())
                .Replace("{{sentence}}", sentences.Joined())
                .Replace("{{language}}", sequence.Language.GetLanguageInFrench())
                .Replace("{{explanation}}", chatGptResponse)
                .Replace("{{modelName}}", this.settings.Value.ModelName);

            return Explanation.Create(
                htmlContent,
                chatGptResponse,
                sequence.ContentToGuessInTargetedLanguage()!, 
                "ChatGpt",
                sequence.Language);
        }

        

        private static string GetContent(ChatGptResponse response)
        {
            return response.choices?.First().Message?.Content is null
                ? "Null content in ChatGpt response"
                : response.choices!.First().Message!.Content!;
        }
        
        public async Task<string> GetSingleWordTranslation(WordSequence wordSequence, Explanation explanationWithChatGpt)
        {
            ChatGptRequest request = new(this.settings.Value.ModelName,
                new()
                {
                    new("user", CreateMessageForSingleWord(wordSequence,explanationWithChatGpt))
                });
            HttpRequestMessage requestMessage = new(HttpMethod.Post, "chat/completions")
            {
                Content = JsonContent.Create(request)
            };
            HttpResponseMessage httpResponseMessage = await this.client.SendAsync(requestMessage);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };
            ChatGptResponse? response = await httpResponseMessage.Content.ReadFromJsonAsync<ChatGptResponse>(options);
            if (response is null) throw new("unexpected ChatGpt response is null for a single word translation");

            string chatGptResponse = GetContent(response);
            const string splitter = "Ce mot se traduit par ";
            if (!chatGptResponse.Contains(splitter))
            {
                return chatGptResponse;
            }

            var singleWordResponse = chatGptResponse.Split(splitter)[1];
            string cleanWord = singleWordResponse.Replace("\"", "");
            cleanWord = cleanWord.Replace(".", "");
            return cleanWord;
        }

        private string CreateMessageForSingleWord(WordSequence wordSequence, Explanation explanationWithChatGpt)
        {
            return $"Dans la description suivante : \"{explanationWithChatGpt.RawExplanation!.Value}\" :  " +
                   $"Peux tu me fournir la traduction en un mot de \"{wordSequence.Word.Value}\" " +
                   $"et retourner ta réponse en commencant par \"ce mot se traduit par \"";
        }
    }
}