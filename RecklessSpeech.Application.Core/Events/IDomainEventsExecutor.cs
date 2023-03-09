namespace RecklessSpeech.Application.Core.Events
{
    public interface IDomainEventsExecutor
    {
        Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents);
    }
}