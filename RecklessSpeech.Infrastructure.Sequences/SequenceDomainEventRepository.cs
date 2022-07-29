﻿using RecklessSpeech.Domain.Sequences;
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
        if (@event is SequencesImportRequestedEvent requestedEvent) await Handle(requestedEvent);
    }

    private async Task Handle(SequencesImportRequestedEvent @event)
    {
        SequenceEntity entity = new(@event.HtmlContent.Value, @event.AudioFileNameWithExtension.Value,
            @event.Tags.Value);
        
        this.dbContext.Sequences.Add(entity);
        await Task.CompletedTask;
    }
}