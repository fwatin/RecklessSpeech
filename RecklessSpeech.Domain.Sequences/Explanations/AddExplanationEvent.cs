using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Explanations;

public record AddExplanationEvent //todo mettre tous les events au participe passé
(
    Explanation Explanation
) : IDomainEvent;