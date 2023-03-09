using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Databases
{
    public class DomainEventsRepository : IDomainEventsRepository
    {
        private readonly IEnumerable<IDomainEventRepository> repositories;

        public DomainEventsRepository(IEnumerable<IDomainEventRepository> repositories) =>
            this.repositories = repositories;

        public async Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (IDomainEvent? domainEvent in domainEvents)
            {
                await Task.WhenAll(
                    this.repositories.Select(repo => repo.ApplyEvent(domainEvent)));
            }
        }
    }
}