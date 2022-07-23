using MediatR;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch;

public record DomainEventIdentifier(Guid EventId, IDomainEvent DomainEvent) : INotification;