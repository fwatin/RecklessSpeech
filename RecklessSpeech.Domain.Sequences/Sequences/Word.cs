namespace RecklessSpeech.Domain.Sequences.Sequences;

public record Word(string Value)
{
    public static Word Hydrate(string value) => new(value);

    public static Word Create(string value)
    {
        return new(value);
    }
}