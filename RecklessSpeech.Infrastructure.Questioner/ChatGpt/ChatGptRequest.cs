namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    public record ChatGptRequest(string model, List<ChatGptMessage> messages);
}