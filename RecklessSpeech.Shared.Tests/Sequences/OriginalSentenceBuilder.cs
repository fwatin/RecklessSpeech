using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class OriginalSentenceBuilder
    {
        public OriginalSentenceBuilder() { }

        public OriginalSentenceBuilder(string value) => this.Value = value;

        public string Value { get; init; } = "It's just a gimmick";

        public static implicit operator OriginalSentence?(OriginalSentenceBuilder? builder)
        {
            if (builder is null)
            {
                return null;
            }

            return new(builder.Value);
        }
    }
}