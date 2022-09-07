namespace RecklessSpeech.Infrastructure.Entities;

public record ExplanationEntity : AggregateRootEntity
{
    public string Word { get; set; }
    public string Value { get; init; }
}