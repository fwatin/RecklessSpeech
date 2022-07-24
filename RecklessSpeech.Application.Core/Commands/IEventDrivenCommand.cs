using MediatR;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Core.Commands;

public interface IEventDrivenCommand : IRequest<IReadOnlyCollection<IDomainEvent>>
{
}