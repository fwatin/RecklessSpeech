namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public interface IDomainEventsRepository
    {
        Task ApplyEvents(IEnumerable<DomainEventIdentifier> domainEvents);
    }
}