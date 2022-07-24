using MediatR;
using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Queries;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch.Transactions;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch;

internal class Dispatcher : IRecklessSpeechDispatcher
{
    private readonly IMediator mediator;
    private readonly IDomainEventsRepository domainEventsRepository;
    private readonly IDomainEventIdProvider domainEventIdProvider;

    public Dispatcher(
        IMediator mediator,
        IDomainEventsRepository domainEventsRepository,
        IDomainEventIdProvider domainEventIdProvider
    )
    {
        this.mediator = mediator;
        this.domainEventsRepository = domainEventsRepository;
        this.domainEventIdProvider = domainEventIdProvider;
    }

    public async Task<IReadOnlyCollection<DomainEventIdentifier>> Dispatch(ITransactionalStrategy transactionalStrategy,
        IEventDrivenCommand command)
    {
        try
        {
            var events = (await this.mediator.Send(command, CancellationToken.None))
                .Select(domainEvent => new DomainEventIdentifier(this.domainEventIdProvider.NewEventId(), domainEvent))
                .ToList();

            await transactionalStrategy.ExecuteTransactional(async () =>
            {
                await this.domainEventsRepository.ApplyEvents(events);
            });
            return events;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query)
    {
        try
        {
            return mediator.Send(query);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Publish(IEnumerable<DomainEventIdentifier> domainEvents)
    {
        foreach (var @event in domainEvents)
        {
            await this.mediator.Publish(@event);
        }
    }
}