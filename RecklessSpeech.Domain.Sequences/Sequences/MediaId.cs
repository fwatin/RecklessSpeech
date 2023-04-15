namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record MediaId(long Value)
    {
        public static MediaId Hydrate(long value) => new(value);

        public static MediaId Create(long value) => new(value);
    }
}