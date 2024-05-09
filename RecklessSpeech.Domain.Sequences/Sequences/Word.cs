namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record Word(string Value) : ISequence
    {
        public static Word Hydrate(string value) => new(value);

        public static Word Create(string value) => new(value);
    }

    public interface ISequence  
    {
        public string Value { get; }
    }
}