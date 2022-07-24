using RecklessSpeech.Application.Core;
using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Queries;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch.Transactions;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch;

public interface IRecklessSpeechDispatcher
{
    Task<IReadOnlyCollection<DomainEventIdentifier>> Dispatch(ITransactionalStrategy transactionalStrategy,
        IEventDrivenCommand command);

    Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query);

    Task Publish(IEnumerable<DomainEventIdentifier> domainEvents);
}