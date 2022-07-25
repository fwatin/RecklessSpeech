namespace RecklessSpeech.Domain.Sequences.Sequences;

public record Tags(string Value)
{
    public static Tags Hydrate(string value) => new(value);
    public static Tags Create(string value) => new(value);
}