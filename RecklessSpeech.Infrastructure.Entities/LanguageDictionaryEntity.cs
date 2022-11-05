namespace RecklessSpeech.Infrastructure.Entities;

public record LanguageDictionaryEntity : AggregateRootEntity
{
    public Guid Id { get; set; }

    public string Url { get; set; }
}