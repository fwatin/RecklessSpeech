using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Explanations;

public record AssignExplanationToSequenceEvent
(
    SequenceId SequenceId,
    Explanation Explanation //todo mettre juste un value type explanationId
) : IDomainEvent;