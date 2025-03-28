using System.Text.Json.Serialization;

namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    // Pour la réponse du chat
    public class ChatResponse
    {
        [JsonPropertyName("choices")] public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("message")] public Message Message { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("role")] public string Role { get; set; }

        [JsonPropertyName("content")] public string Content { get; set; }

        [JsonPropertyName("function_call")] public FunctionCall FunctionCall { get; set; }
    }

    public class FunctionCall
    {
        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("arguments")] public string Arguments { get; set; }
    }

    // Modèle pour extraire le "cards" dans function_call.arguments
    public class CreateCardsArgs
    {
        [JsonPropertyName("cards")] public List<Card> Cards { get; set; }
    }

    public class Card
    {
        [JsonPropertyName("question")] public string Question { get; set; }

        [JsonPropertyName("answer")] public string Answer { get; set; }
    }
}