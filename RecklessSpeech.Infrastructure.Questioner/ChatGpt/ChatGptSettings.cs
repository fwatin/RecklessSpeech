using System.ComponentModel.DataAnnotations;

namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    public class ChatGptSettings
    {
        public const string SECTION_KEY = "ChatGpt";
        
        public string SubscriptionKey { get; set; }

        [Required] public string Url { get; set; }
        [Required] public string ModelName { get; set; }
    }
}