namespace RecklessSpeech.Infrastructure.Entities;

public abstract record AggregateRootEntity
{
    public Guid Id { get; set; }
    public uint xmin { get; set; }
}