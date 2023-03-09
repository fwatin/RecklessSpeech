namespace RecklessSpeech.Domain.Sequences.Notes
{
    public record Audio(string Value)
    {
        public static Audio Create(string value) => new(value);
    }
}