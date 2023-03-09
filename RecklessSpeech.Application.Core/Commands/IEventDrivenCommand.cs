using MediatR;
using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Application.Core.Commands
{
    public interface IEventDrivenCommand : IRequest<IReadOnlyCollection<IDomainEvent>>
    {
    }
}