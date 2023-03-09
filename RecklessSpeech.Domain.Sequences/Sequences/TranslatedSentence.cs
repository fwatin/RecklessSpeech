namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record TranslatedSentence(string Value)
    {
        public static TranslatedSentence Hydrate(string value) => new(value);

        public static TranslatedSentence Create(string value) => new(value);
    }
}