namespace RecklessSpeech.Domain.Sequences.Notes;

public record Source(string Value)
{
    public static Source Create(string value)
    {
        return new(value);
    }
}