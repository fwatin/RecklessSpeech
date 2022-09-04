using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
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
        Sequence sequence = await sequenceRepository.GetOne(command.sequenceId);

        Explanation explanation = translatorGateway.GetExplanation(sequence.Word.Value);

        var events = new[]
        {
            new EnrichSequenceEvent(command.sequenceId, explanation)
        };
        
        return await Task.FromResult(events);
    }
}