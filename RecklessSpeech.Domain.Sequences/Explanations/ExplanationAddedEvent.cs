using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Domain.Sequences.Explanations
{
    public record ExplanationAddedEvent
    (
        Explanation Explanation
    ) : IDomainEvent;
}