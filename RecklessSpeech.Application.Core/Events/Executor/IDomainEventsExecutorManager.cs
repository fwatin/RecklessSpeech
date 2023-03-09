namespace RecklessSpeech.Application.Core.Events.Executor
{
    public interface IDomainEventsExecutorManager
    {
        Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents);
    }
}