using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record AssignLanguageDictionaryCommand(Guid SequenceId, Guid DictionaryId) : IEventDrivenCommand;

public class AssignLanguageDictionaryCommandHandler : CommandHandlerBase<AssignLanguageDictionaryCommand>
{
    private readonly ISequenceRepository sequenceRepository;
    
    public AssignLanguageDictionaryCommandHandler(
        ISequenceRepository sequenceRepository)
    {
        this.sequenceRepository = sequenceRepository;

    }
    protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(AssignLanguageDictionaryCommand command)
    {
        Sequence? sequence = await this.sequenceRepository.GetOne(command.SequenceId);
        if (sequence is null) throw new Exception("the sequence does not exist.");
        
        IEnumerable<IDomainEvent> events = sequence.SetDictionary(command.DictionaryId);
        
        return events.ToList();
    }
}