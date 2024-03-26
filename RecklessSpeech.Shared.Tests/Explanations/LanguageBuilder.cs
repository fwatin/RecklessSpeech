using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Shared.Tests.Explanations
{
    public class LanguageBuilder
    {
        public LanguageBuilder() { }

        public LanguageBuilder(Language value) => this.Value = value;

        public Language Value { get; init; } = new Dutch();

        public static implicit operator Language(LanguageBuilder urlBuilder) => urlBuilder.Value;
    }
}