using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Databases
{
    public class DomainEventsRepository : IDomainEventsRepository
    {
        private readonly IEnumerable<IDomainEventRepository> repositories;

        public DomainEventsRepository(IEnumerable<IDomainEventRepository> repositories) =>
            this.repositories = repositories;

        public async Task ApplyEvents(IEnumerable<DomainEventIdentifier> domainEvents)
        {
            foreach (IDomainEvent? domainEvent in domainEvents.Select(x => x.DomainEvent))
            {
                await Task.WhenAll(
                    this.repositories.Select(repo => repo.ApplyEvent(domainEvent)));
            }
        }
    }
}