using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class OriginalSentenceBuilder
    {
        public OriginalSentenceBuilder() => this.Value = new() { "", "It's just a gimmick", "" };

        public OriginalSentenceBuilder(List<string> value) => this.Value = value;

        private List<string> Value { get; }

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