using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Shared.Tests.Explanations
{
    public class ContentBuilder
    {
        public ContentBuilder() { }

        public ContentBuilder(string value) => this.Value = value;

        public string Value { get; init; } = "veut dire genre trucs, astuces";

        public static implicit operator Content(ContentBuilder builder) => new(builder.Value);
    }
}