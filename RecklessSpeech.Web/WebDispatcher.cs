using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Dispatch;
using RecklessSpeech.Application.Core.Dispatch.Transactions;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Core.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecklessSpeech.Web
{
    public class WebDispatcher
    {
        private readonly IDispatcher dispatcher;

        public WebDispatcher(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        public Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query) => this.dispatcher.Dispatch(query);

        public async Task Dispatch(IEventDrivenCommand command)
        {
            IReadOnlyCollection<IDomainEvent> domainEvents =
                await this.dispatcher.Dispatch(new RootTransactionalStrategy(), command);
            await this.Publish(domainEvents);
        }

        public Task Publish(IEnumerable<IDomainEvent> domainEvents) => this.dispatcher.Publish(domainEvents);
    }
}