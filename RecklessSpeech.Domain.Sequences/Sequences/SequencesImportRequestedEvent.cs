using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public record SequencesImportRequestedEvent
(
    SequenceId Id,
    HtmlContent HtmlContent,
    AudioFileNameWithExtension AudioFileNameWithExtension,
    Tags Tags
) : IDomainEvent;