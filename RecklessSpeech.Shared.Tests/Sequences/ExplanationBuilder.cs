using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

namespace RecklessSpeech.Shared.Tests.Sequences;

public class ExplanationBuilder
{
    public ExplanationBuilder() { }
    public ExplanationBuilder(string? value)
    {
        this.Value = value;
    }
    
    public string? Value { get; set; } = null;

    public static implicit operator Explanation(ExplanationBuilder builder) => new(builder.Value);
}