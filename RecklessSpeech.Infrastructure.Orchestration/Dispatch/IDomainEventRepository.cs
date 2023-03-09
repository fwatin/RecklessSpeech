using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public interface IDomainEventExecutor
    {
        Task ApplyEvent(IDomainEvent domainEvent);
    }
}