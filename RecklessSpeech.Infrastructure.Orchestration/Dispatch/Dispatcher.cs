using MediatR;
using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Core.Queries;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch.Transactions;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    internal class Dispatcher : IRecklessSpeechDispatcher
    {
        private readonly IDomainEventsExecutor domainEventsExecutor;
        private readonly IMediator mediator;

        public Dispatcher(IMediator mediator, IDomainEventsExecutor domainEventsExecutor)
        {
            this.mediator = mediator;
            this.domainEventsExecutor = domainEventsExecutor;
        }

        public async Task<IReadOnlyCollection<IDomainEvent>> Dispatch(
            ITransactionalStrategy transactionalStrategy,
            IEventDrivenCommand command)
        {
            try
            {
                List<IDomainEvent> events = (await this.mediator.Send(command, CancellationToken.None))
                    .ToList();

                await transactionalStrategy.ExecuteTransactional(async () =>
                {
                    await this.domainEventsExecutor.ApplyEvents(events);
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
                return this.mediator.Send(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Publish(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (IDomainEvent? @event in domainEvents)
            {
                await this.mediator.Publish(@event);
            }
        }
    }
}