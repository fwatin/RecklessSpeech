using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class OriginalSentenceBuilder
    {
        public OriginalSentenceBuilder() { }

        public OriginalSentenceBuilder(List<string> value) => this.Value = value;

        public List<string> Value { get; init; } = new(){"It's just a gimmick"};

        public static implicit operator OriginalSentences?(OriginalSentenceBuilder? builder)
        {
            if (builder is null)
            {
                return null;
            }

            return new(builder.Value);
        }
    }
}