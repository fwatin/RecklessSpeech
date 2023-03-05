namespace RecklessSpeech.Domain.Sequences.Notes;

public record Answer(string Value)
{
    public static Answer Create(string value)
    {
        return new Answer(value);
    }
}