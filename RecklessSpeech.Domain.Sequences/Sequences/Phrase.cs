namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record Phrase(string Value) : ISequence
    {
        public static Phrase Hydrate(string value) => new(value);

        public static Phrase Create(string value) => new(value);
    }
}