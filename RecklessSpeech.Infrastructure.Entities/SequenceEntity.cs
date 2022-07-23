namespace RecklessSpeech.Infrastructure.Entities;

public record SequenceEntity(string HtmlContent, string AudioFileNameWithExtension, string Tags) : AggregateRootEntity;