using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public record EnrichSequenceEvent //todo value object for sequenceId
(
    Guid sequenceId,
    Explanation Explanation
) : IDomainEvent;