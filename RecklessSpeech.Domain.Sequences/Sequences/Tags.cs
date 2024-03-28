namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record Tag(string Value)
    {
        public static Tag Hydrate(string value) => new(value);
        public static Tag Create(string value) => new(value);
    }
}