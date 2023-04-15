namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record TranslatedSentence(string Value)
    {
        public static TranslatedSentence Hydrate(string value) => new(value);

        public static TranslatedSentence Create(string value) => new(value);
    }
    
    public record MediaId(string Value)
    {
        public static MediaId Hydrate(string value) => new(value);

        public static MediaId Create(string value) => new(value);
    }
}