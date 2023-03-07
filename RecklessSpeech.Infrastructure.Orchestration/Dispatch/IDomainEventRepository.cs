using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public interface IDomainEventRepository
    {
        Task ApplyEvent(IDomainEvent domainEvent);
    }
}