using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Databases;

public class EntityFrameworkDomainEventsRepository:IDomainEventsRepository
{
    private InMemoryRecklessSpeechDbContext dbContext;
    private readonly IEnumerable<IDomainEventRepository> repositories;
    
    public EntityFrameworkDomainEventsRepository(InMemoryRecklessSpeechDbContext dbContext, IEnumerable<IDomainEventRepository> repositories)
    {
        this.dbContext = dbContext;
        this.repositories = repositories;
    }

    public async Task ApplyEvents(IEnumerable<DomainEventIdentifier> domainEvents)
    {
        foreach (var domainEvent in domainEvents.Select(x=>x.DomainEvent))
        {
            await Task.WhenAll(
                this.repositories.Select(repo => repo.ApplyEvent(domainEvent)));
        }

        //await dbContext.SaveChangesAsync();
    }
}