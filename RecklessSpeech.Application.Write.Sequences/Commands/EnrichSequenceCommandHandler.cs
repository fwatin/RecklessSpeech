using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record EnrichSequenceCommand(Guid sequenceId) : IEventDrivenCommand;

public class EnrichSequenceCommandHandler : CommandHandlerBase<EnrichSequenceCommand>
{
    private readonly ISequenceQueryRepository sequenceRepository; //todo utiliser le sequence repository in mem
    private readonly ITranslatorGateway translatorGateway;

    public EnrichSequenceCommandHandler(ISequenceQueryRepository sequenceQueryRepository,
        ITranslatorGateway translatorGateway)
    {
        this.sequenceRepository = sequenceQueryRepository;
        this.translatorGateway = translatorGateway;
    }

    protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(EnrichSequenceCommand command)
    {
        SequenceSummaryQueryModel? sequence = this.sequenceRepository.TryGetOne(command.sequenceId);

        Explanation explanation = this.translatorGateway.GetExplanation(sequence!.Word);

        EnrichSequenceEvent[] events =
        {
            new(command.sequenceId, explanation)
        };

        return await Task.FromResult(events);
    }
}