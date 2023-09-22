using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class TranslatedSentenceBuilder
    {
        public TranslatedSentenceBuilder()
        {
            this.Value = "Et ça n'arrive pas par quelques astuces statistiques.";
        }

        public TranslatedSentenceBuilder(string value) => this.Value = value;

        public string Value { get; }

        public static implicit operator SentenceTranslations(TranslatedSentenceBuilder builder) =>
            SentenceTranslations.Create(new[] { "", builder.Value, "" },
                new[] { "", builder.Value, "" });
    }
}