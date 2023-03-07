namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch
{
    public interface IDomainEventIdProvider
    {
        Guid NewEventId();
    }
}