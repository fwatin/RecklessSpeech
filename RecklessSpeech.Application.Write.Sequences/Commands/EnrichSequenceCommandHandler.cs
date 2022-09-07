﻿using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record EnrichSequenceCommand(Guid SequenceId) : IEventDrivenCommand;

public class EnrichSequenceCommandHandler : CommandHandlerBase<EnrichSequenceCommand>
{
    private readonly ISequenceRepository sequenceRepository;
    private readonly ITranslatorGateway translatorGateway;

    public EnrichSequenceCommandHandler(ISequenceRepository sequenceRepository,
        ITranslatorGateway translatorGateway)
    {
        this.sequenceRepository = sequenceRepository;
        this.translatorGateway = translatorGateway;
    }

    protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(EnrichSequenceCommand command)
    {
        Sequence sequence = await this.sequenceRepository.GetOne(command.SequenceId);

        Explanation explanation = this.translatorGateway.GetExplanation(sequence.Word.Value);

        IDomainEvent[] events =
        {
            new ExplanationAddedEvent(explanation),
            new ExplanationAssignedToSequenceEvent(sequence.SequenceId, explanation.ExplanationId)
        };

        return events;
    }
}