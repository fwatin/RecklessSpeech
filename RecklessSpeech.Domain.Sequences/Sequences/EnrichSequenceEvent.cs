using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public record EnrichSequenceEvent
(
    Guid sequenceId,
    Explanation Explanation
) : IDomainEvent;