namespace RecklessSpeech.Application.Core.Events
{
    public class DomainEventsExecutorManager : IDomainEventsExecutorManager
    {
        private readonly IEnumerable<IDomainEventExecutor> repositories;

        public DomainEventsExecutorManager(IEnumerable<IDomainEventExecutor> repositories) =>
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