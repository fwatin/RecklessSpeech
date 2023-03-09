using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Domain.Sequences.Explanations
{
    public record ExplanationAssignedToSequenceEvent
    (
        SequenceId SequenceId,
        ExplanationId ExplanationId
    ) : IDomainEvent;
}