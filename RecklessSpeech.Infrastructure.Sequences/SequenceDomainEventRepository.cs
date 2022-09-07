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
            case AddedSequenceEvent requestedEvent:
                await Handle(requestedEvent);
                break;
            
            case ExplanationAssignedToSequenceEvent enrichSequenceEvent:
                await Handle(enrichSequenceEvent);
                break;
            
            case ExplanationAddedEvent addExplanationEvent:
                await Handle(addExplanationEvent);
                break;
            
            default: throw new Exception("event type is not known for ApplyEvent");
        }
    }

    private async Task Handle(AddedSequenceEvent @event)
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
    
    private async Task Handle(ExplanationAddedEvent addedEvent)
    {
        ExplanationEntity entity = new()
        {
            Id = addedEvent.Explanation.ExplanationId.Value,
            Value = addedEvent.Explanation.Content.Value,
            Word = addedEvent.Explanation.Word.Value
        };
        
        this.dbContext.Explanations.Add(entity); //passer en addAsync plus tard quand EF
        await Task.CompletedTask;
    }

    private async Task Handle(ExplanationAssignedToSequenceEvent @event)
    {
        SequenceEntity sequenceEntity = this.dbContext.Sequences.Single(x => x.Id == @event.SequenceId.Value);

        sequenceEntity.Explanation = @event.Explanation.ExplanationId.Value;

        await Task.CompletedTask;
    }
}