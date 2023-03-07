namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public class DomainEventIdProvider : IDomainEventIdProvider
    {
        public Guid NewEventId() => Guid.NewGuid();
    }
}