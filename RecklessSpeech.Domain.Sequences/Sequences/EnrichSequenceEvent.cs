using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public record EnrichSequenceEvent
(
    SequenceId SequenceId,
    Explanation Explanation
) : IDomainEvent;