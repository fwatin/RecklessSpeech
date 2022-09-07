using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public class ExplanationBuilder
{
    public ExplanationBuilder() { }
    public ExplanationBuilder(string? value)
    {
        this.Value = value;
    }

    public string Value { get; set; } = "veut dire genre trucs, astuces";
    public string Word { get; set; } = "gimmicks";
    public static implicit operator Explanation(ExplanationBuilder builder) => Explanation.Create(builder.Value, builder.Word);
}