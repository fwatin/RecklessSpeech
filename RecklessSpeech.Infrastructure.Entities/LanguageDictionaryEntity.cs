namespace RecklessSpeech.Infrastructure.Entities
{
    public record LanguageDictionaryEntity : AggregateRootEntity
    {
        public string Url { get; init; }
        public string Name { get; init; }
        public string FromLanguage { get; init; }
        public string ToLanguage { get; init; }
    }
}