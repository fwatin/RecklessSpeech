namespace RecklessSpeech.Application.Core.Events
{
    public interface IDomainEventExecutor
    {
        Task ApplyEvent(IDomainEvent domainEvent);
    }
}