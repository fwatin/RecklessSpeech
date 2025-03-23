namespace RecklessSpeech.Domain.Questioner
{
    public record Question(string Value)
    {
        public static Question Create(string value) => new(value);
    }
}