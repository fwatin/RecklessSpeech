﻿namespace RecklessSpeech.Infrastructure.Entities;

public record SequenceEntity : AggregateRootEntity
{
    public string HtmlContent { get; init; } = default!;
    public string AudioFileNameWithExtension { get; init; } = default!;
    public string Tags { get; init; } = default!;
    public string Word { get; init; } = default!;
    public Guid? ExplanationId { get; set; }
}