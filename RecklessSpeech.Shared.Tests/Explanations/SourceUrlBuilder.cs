using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Explanations
{
    public class SourceUrlBuilder
    {
        public SourceUrlBuilder() { }

        public SourceUrlBuilder(string value) => this.Value = value;

        public string Value { get; set; } = "https://www.mijnwoordenboek.nl/vertaal/NL/FR/gimmicks";

        public static implicit operator Source(SourceUrlBuilder urlBuilder) => new(urlBuilder.Value);
    }
}