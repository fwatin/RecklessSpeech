using System.ComponentModel.DataAnnotations;
namespace RecklessSpeech.Infrastructure.Questioner
{
    public class AnkiQuestionerSettings
    {
        public const string SECTION_KEY = "Anki";

        [Required] public string Url { get; set; }
    }
}