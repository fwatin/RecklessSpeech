using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public class TranslatedWordBuilder
{
    public TranslatedWordBuilder() { }
    public TranslatedWordBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; set; } = "astuce";

    public static implicit operator TranslatedWord(TranslatedWordBuilder builder) => new(builder.Value);
}