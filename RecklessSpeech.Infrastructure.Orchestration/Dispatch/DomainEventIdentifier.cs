using MediatR;
using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public record DomainEventIdentifier(Guid EventId, IDomainEvent DomainEvent) : INotification;
} //todo dégager cette tuyauterie trop compliqué-gérer qu'un seul type d'event