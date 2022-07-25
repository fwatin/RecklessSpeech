using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public record SequencesImportRequestedEvent
(
    HtmlContent HtmlContent,
    AudioFileNameWithExtension AudioFileNameWithExtension,
    Tags Tags
) : IDomainEvent;