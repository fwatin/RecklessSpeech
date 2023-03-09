namespace RecklessSpeech.Application.Core.Events.Executor
{
    public interface IDomainEventExecutor
    {
        Task ApplyEvent(IDomainEvent domainEvent);
    }
}