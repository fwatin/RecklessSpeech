namespace RecklessSpeech.Application.Core.Events
{
    public interface IDomainEventsExecutorManager //todo à virer n'utiliser que la classe
    {
        Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents);
    }
}