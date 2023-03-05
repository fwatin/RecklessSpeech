using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record EnrichDutchSequenceCommand(Guid SequenceId) : IEventDrivenCommand;

public class EnrichDutchSequenceCommandHandler : CommandHandlerBase<EnrichDutchSequenceCommand>
{
    private readonly ISequenceRepository sequenceRepository;
    private readonly IExplanationRepository explanationRepository;
    private readonly IDutchTranslatorGateway dutchTranslatorGateway;

    public EnrichDutchSequenceCommandHandler(
        ISequenceRepository sequenceRepository,
        IExplanationRepository explanationRepository,
        IDutchTranslatorGateway dutchTranslatorGateway)
    {
        this.sequenceRepository = sequenceRepository;
        this.explanationRepository = explanationRepository;
        this.dutchTranslatorGateway = dutchTranslatorGateway;
    }

    protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(EnrichDutchSequenceCommand command)
    {
        Sequence? sequence = await this.sequenceRepository.GetOne(command.SequenceId);

        Explanation? existingExplanation = this.explanationRepository.TryGetByTarget(sequence!.Word.Value);

        Explanation explanation = existingExplanation ?? this.dutchTranslatorGateway.GetExplanation(sequence.Word.Value);

        List<IDomainEvent> events = new()
        {
            new ExplanationAssignedToSequenceEvent(sequence.SequenceId, explanation.ExplanationId)
        };
        if (existingExplanation is null)
        {
            events.Add(new ExplanationAddedEvent(explanation));
        }

        return events;
    }
}