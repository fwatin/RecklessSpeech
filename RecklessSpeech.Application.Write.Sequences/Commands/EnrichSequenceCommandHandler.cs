﻿using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record EnrichSequenceCommand(Guid sequenceId) : IEventDrivenCommand;

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
        Sequence sequence = await this.sequenceRepository.GetOne(command.sequenceId);

        Explanation explanation = this.translatorGateway.GetExplanation(sequence.Word.Value);

        IDomainEvent[] events =
        {
            new AddExplanationEvent(explanation),
            new AssignExplanationToSequenceEvent(new SequenceId(command.sequenceId), explanation)
        };
        //todo shooter deux events : un pour ajouter selon condition et un pour assigner + et faire des tests accep selon les cas
        return events;
    }
}