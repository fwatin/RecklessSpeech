namespace RecklessSpeech.Domain.Questioner
{
    public record After(string Value)
    {
        public static After Create(string value) => new(value);
    }
}