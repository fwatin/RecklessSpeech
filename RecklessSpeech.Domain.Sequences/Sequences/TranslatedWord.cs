namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record TranslatedWord(string Value)
    {
        public static TranslatedWord? Hydrate(string? value)
        {
            if (value is null) return null;
            return new(value);
        }

        public static TranslatedWord Create(string value) => new(value);
    }
}