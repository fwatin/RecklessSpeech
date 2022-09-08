using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Explanations;

public record ExplanationAddedEvent
(
    Explanation Explanation
) : IDomainEvent;