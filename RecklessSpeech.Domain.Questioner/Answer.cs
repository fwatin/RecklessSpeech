namespace RecklessSpeech.Domain.Questioner
{
    public record Answer(string Value)
    {
        public static Answer Create(string value) => new(value);
    }
}