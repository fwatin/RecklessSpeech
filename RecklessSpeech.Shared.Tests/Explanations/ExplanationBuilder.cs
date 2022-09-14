using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Shared.Tests.Explanations;

public record ExplanationBuilder
{
    public ExplanationIdBuilder ExplanationId { get; init; }
    public ContentBuilder Content { get; init; }
    public TargetBuilder Target { get; init; }
    public SourceUrlBuilder SourceUrl { get; init; }

    private ExplanationBuilder(
        ExplanationIdBuilder explanationId,
        ContentBuilder content,
        TargetBuilder target,
        SourceUrlBuilder sourceUrl
    )
    {
        this.ExplanationId = explanationId;
        this.Content = content;
        this.Target = target;
        this.SourceUrl = sourceUrl;
    }

    public static ExplanationBuilder Create(Guid id)
    {
        return new ExplanationBuilder(
            new(id),
            new(),
            new(),
            new());
    }
    public static implicit operator Explanation(ExplanationBuilder builder) =>
        Explanation.Create(
            builder.ExplanationId.Value,
            builder.Content.Value,
            builder.Target.Value,
            builder.SourceUrl.Value);

    public ExplanationEntity BuildEntity()
    {
        return new ExplanationEntity()
        {
            Id = this.ExplanationId.Value,
            Content = this.Content.Value,
            Target = this.Target.Value,
            SourceUrl = this.SourceUrl.Value
        };
    }
}