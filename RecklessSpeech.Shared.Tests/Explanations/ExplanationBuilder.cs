using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Shared.Tests.Explanations;

public class ExplanationBuilder
{
    public ExplanationIdBuilder ExplanationId { get; init; }
    public ContentBuilder Content { get; init; }
    public TargetBuilder Target { get; init; }

    private ExplanationBuilder(
        ExplanationIdBuilder explanationId,
        ContentBuilder content,
        TargetBuilder target
    )
    {
        this.ExplanationId = explanationId;
        this.Content = content;
        this.Target = target;
    }

    public static ExplanationBuilder Create(Guid id)
    {
        return new ExplanationBuilder(
            new(id),
            new(),
            new());
    }
    public static implicit operator Explanation(ExplanationBuilder builder) =>
        Explanation.Create(
            builder.ExplanationId.Value,
            builder.Content.Value,
            builder.Target.Value);
}