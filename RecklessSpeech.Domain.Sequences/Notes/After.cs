namespace RecklessSpeech.Domain.Sequences.Notes;

public record After(string Value)
{
    public static After Create(string value)
    {
        return new(value);
    }
}