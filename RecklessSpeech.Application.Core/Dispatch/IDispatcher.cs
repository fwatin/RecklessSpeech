using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Core.Queries;

namespace RecklessSpeech.Application.Core.Dispatch
{
    public interface IDispatcher
    {
        Task<IReadOnlyCollection<IDomainEvent>> Dispatch(ITransactionalStrategy transactionalStrategy,
            IEventDrivenCommand command);

        Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query);

        Task Publish(IEnumerable<IDomainEvent> domainEvents);
    }
}