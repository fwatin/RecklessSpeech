namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch;

public class DomainEventIdProvider : IDomainEventIdProvider
{
    public Guid NewEventId()
    {
        return Guid.NewGuid();
    }
}