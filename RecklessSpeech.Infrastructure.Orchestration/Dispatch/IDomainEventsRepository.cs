using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public interface IDomainEventsExecutor
    {
        Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents);
    }
}