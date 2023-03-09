using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Databases
{
    public class DomainEventsExecutor : IDomainEventsExecutor
    {
        private readonly IEnumerable<IDomainEventExecutor> repositories;

        public DomainEventsExecutor(IEnumerable<IDomainEventExecutor> repositories) =>
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