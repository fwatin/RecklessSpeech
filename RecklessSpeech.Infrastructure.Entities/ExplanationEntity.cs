namespace RecklessSpeech.Infrastructure.Entities;

public record ExplanationEntity : AggregateRootEntity
{
    public string Target { get; set; } = default!;
    public string Content { get; init; } = default!;
}