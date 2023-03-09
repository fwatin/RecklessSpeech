using MediatR;
using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Application.Core.Commands
{
    public abstract class CommandHandlerBase<TCommand> : IRequestHandler<TCommand, IReadOnlyCollection<IDomainEvent>>
        where TCommand : IEventDrivenCommand
    {
        public async Task<IReadOnlyCollection<IDomainEvent>> Handle(TCommand request,
            CancellationToken cancellationToken) => await this.Handle(request);

        protected abstract Task<IReadOnlyCollection<IDomainEvent>> Handle(TCommand command);
    }
}