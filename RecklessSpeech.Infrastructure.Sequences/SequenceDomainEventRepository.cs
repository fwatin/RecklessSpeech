using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Sequences;

public class SequenceDomainEventRepository : IDomainEventRepository
{
    private readonly ISequencesDbContext dbContext;

    public SequenceDomainEventRepository(ISequencesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case SequencesImportRequestedEvent requestedEvent:
                await Handle(requestedEvent);
                break;
            
            case AssignExplanationToSequenceEvent enrichSequenceEvent:
                await Handle(enrichSequenceEvent);
                break;
            
            case AddExplanationEvent addExplanationEvent:
                await Handle(addExplanationEvent);
                break;
            
            default: throw new Exception("event type is not known for ApplyEvent");
        }
    }

    private async Task Handle(SequencesImportRequestedEvent @event)
    {
        SequenceEntity entity = new()
        {
            Id = @event.Id.Value,
            HtmlContent = @event.HtmlContent.Value,
            AudioFileNameWithExtension = @event.AudioFileNameWithExtension.Value,
            Tags = @event.Tags.Value,
            Word = @event.Word.Value
        };

        this.dbContext.Sequences.Add(entity);
        await Task.CompletedTask;
    }
    
    private async Task Handle(AddExplanationEvent @event)
    {
        ExplanationEntity entity = new()
        {
            Id = @event.Explanation.Id,
            Value = @event.Explanation.Value,
            Word = @event.Explanation.Word
        };
        
        this.dbContext.Explanations.Add(entity); //passer en addAsync plus tard quand EF
        await Task.CompletedTask;
    }

    private async Task Handle(AssignExplanationToSequenceEvent @event)
    {
        SequenceEntity sequenceEntity = this.dbContext.Sequences.Single(x => x.Id == @event.SequenceId.Value);

        sequenceEntity.Explanation = @event.Explanation.Id;

        await Task.CompletedTask;
    }
}