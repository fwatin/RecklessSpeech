namespace RecklessSpeech.Infrastructure.Entities;

public record LanguageDictionaryEntity : AggregateRootEntity
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string FromLanguage { get; set; }
    public string ToLanguage { get; set; }
}