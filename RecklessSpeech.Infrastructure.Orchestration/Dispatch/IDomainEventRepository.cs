using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public interface IDomainEventRepository
    {
        Task ApplyEvent(IDomainEvent domainEvent);
    }
}