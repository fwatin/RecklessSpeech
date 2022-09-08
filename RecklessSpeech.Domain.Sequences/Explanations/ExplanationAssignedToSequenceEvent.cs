using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Explanations;

public record ExplanationAssignedToSequenceEvent
(
    SequenceId SequenceId,
    ExplanationId ExplanationId
) : IDomainEvent;