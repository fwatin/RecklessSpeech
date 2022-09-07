using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Shared.Tests.Sequences;

public class ExplanationBuilder
{
    public ExplanationBuilder(Guid id) //todo mettre un Create
    {
        this.Id = id;
    }

    public string Value { get; set; } = "veut dire genre trucs, astuces"; //todo mettre des value type
    public string Word { get; set; } = "gimmicks";//todo mettre des value type
    public Guid Id { get; set; }
    public static implicit operator Explanation(ExplanationBuilder builder) => Explanation.Create(builder.Id, builder.Value, builder.Word);
}