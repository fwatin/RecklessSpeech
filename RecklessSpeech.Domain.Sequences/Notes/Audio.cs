namespace RecklessSpeech.Domain.Sequences.Notes;

public record Audio(string Value)
{
    public static Audio Create(string value)
    {
        return new Audio(value);
    }
}