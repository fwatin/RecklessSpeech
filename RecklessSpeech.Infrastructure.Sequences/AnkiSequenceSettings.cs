using System.ComponentModel.DataAnnotations;

namespace RecklessSpeech.Infrastructure.Sequences
{
    public class AnkiSequenceSettings
    {
        public const string SECTION_KEY = "Anki";

        [Required] public string Url { get; set; }

        [Required] public string MediaPath { get; set; }
    }
}