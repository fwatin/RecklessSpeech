namespace RecklessSpeech.Infrastructure.Entities
{
    public abstract record AggregateRootEntity //todo rename to dao
    {
        public Guid Id { get; init; }
    }
}