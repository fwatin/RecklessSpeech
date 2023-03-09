using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public interface IDomainEventsRepository
    {
        Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents);
    }
}