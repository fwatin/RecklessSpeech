using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Sequences.Tests;

public class BaseInfrastructureTests    
{
    protected InMemorySequencesDbContext InMemorySequencesDbContext { get; }
    protected DomainEventsRepository Sut{ get; }
    protected BaseInfrastructureTests()
    {
        this.InMemorySequencesDbContext = new();
        this.Sut = new(new IDomainEventRepository[]
        {
            new SequenceDomainEventRepository(this.InMemorySequencesDbContext)
        });
    }
    protected async Task ApplyEvent(IDomainEvent newEvent)
    {
        await this.Sut.ApplyEvents(new List<DomainEventIdentifier>()
        {
            new(Guid.NewGuid(), newEvent)
        });
    }
}