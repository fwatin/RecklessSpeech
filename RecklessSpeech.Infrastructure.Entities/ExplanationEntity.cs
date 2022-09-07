namespace RecklessSpeech.Infrastructure.Entities;

public record ExplanationEntity : AggregateRootEntity
{
    public string Word { get; set; } = default!;
    public string Value { get; init; } = default!;
}