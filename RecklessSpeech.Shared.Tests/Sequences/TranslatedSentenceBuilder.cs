using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class TranslatedSentenceBuilder
    {
        public TranslatedSentenceBuilder() { }

        public TranslatedSentenceBuilder(string value) => this.Value = value;

        public string Value { get; set; } = "Et ça n'arrive pas par quelques astuces statistiques.";

        public static implicit operator TranslatedSentence(TranslatedSentenceBuilder builder) => new(builder.Value);
    }
}