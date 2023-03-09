using MediatR;
using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Core.Events.Executor;
using RecklessSpeech.Application.Core.Queries;

namespace RecklessSpeech.Application.Core.Dispatch
{
    internal class Dispatcher : IDispatcher
    {
        private readonly IDomainEventsExecutorManager executorManager;
        private readonly IMediator mediator;

        public Dispatcher(IMediator mediator, IDomainEventsExecutorManager executorManager)
        {
            this.mediator = mediator;
            this.executorManager = executorManager;
        }

        public async Task<IReadOnlyCollection<IDomainEvent>> Dispatch(
            ITransactionalStrategy transactionalStrategy,
            IEventDrivenCommand command)
        {
            try
            {
                IReadOnlyCollection<IDomainEvent> events = (await this.mediator.Send(command, CancellationToken.None));

                await transactionalStrategy.ExecuteTransactionInReadCommitted(async () =>
                {
                    await this.executorManager.ApplyEvents(events);
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