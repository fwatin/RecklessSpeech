namespace RecklessSpeech.Infrastructure.Sequences.Gateways.ChatGpt
{
    public record ChatGptRequest(string model, List<ChatGptMessage> messages);
}