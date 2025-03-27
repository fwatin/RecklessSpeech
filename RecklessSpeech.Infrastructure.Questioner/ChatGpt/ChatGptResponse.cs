namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    public class ChatGptResponse
    {
        public string? Id { get; set; }
        public string? Object { get; set; }
        public int created { get; set; }
        public string? model { get; set; }
        public Usage? usage { get; set; }
        public Choice[]? choices { get; set; }
    }

    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }

    public class Choice
    {
        public Message? Message { get; set; }
        public string? FinishReason { get; set; }
        public int index { get; set; }
    }

    public class Message
    {
        public string? Role { get; set; }
        public string? Content { get; set; }
    }
}