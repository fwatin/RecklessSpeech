namespace RecklessSpeech.Infrastructure.Entities;

public record SequenceEntity : AggregateRootEntity
{
    public string HtmlContent { get; init; }
    public string AudioFileNameWithExtension { get; init; }
    public string Tags { get; init; }
    
    public string Word { get; init; }
}