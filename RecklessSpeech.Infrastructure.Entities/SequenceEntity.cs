namespace RecklessSpeech.Infrastructure.Entities;

public record SequenceEntity : AggregateRootEntity
{
    public string HtmlContent { get; init; } //todo traiter tous les warnings
    public string AudioFileNameWithExtension { get; init; }
    public string Tags { get; init; }
    public string Word { get; init; }
    public Guid? Explanation { get; set; }
}