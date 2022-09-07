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
            
            case EnrichSequenceEvent enrichSequenceEvent:
                await Handle(enrichSequenceEvent);
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

    private async Task Handle(EnrichSequenceEvent @event)
    {
        SequenceEntity entity = this.dbContext.Sequences.Single(x => x.Id == @event.sequenceId);
        entity.Explanation = @event.Explanation.Value;
        await Task.CompletedTask;
    }
}