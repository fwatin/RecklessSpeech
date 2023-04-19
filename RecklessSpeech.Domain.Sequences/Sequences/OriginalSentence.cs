namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record OriginalSentence(string Value)
    {
        public static OriginalSentence? Hydrate(string? value)
        {
            if (value is null)
            {
                return null;
            }

            return new(value);
        }

        public static OriginalSentence Create(string value) => new(value);
    }
}