namespace RecklessSpeech.Infrastructure.Entities
{
    public abstract record RootDao
    {
        public Guid Id { get; protected init; }
    }
}