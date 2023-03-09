namespace RecklessSpeech.Application.Core.Events.Executor
{
    public interface IDomainEventsExecutorManager //todo à virer n'utiliser que la classe
    {
        Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents);
    }
}