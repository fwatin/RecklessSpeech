using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class SequenceIdBuilder
    {
        public SequenceIdBuilder(Guid value) => this.Value = value;

        public SequenceIdBuilder() => this.Value = Guid.NewGuid();

        public Guid Value { get; }

        public static implicit operator SequenceId(SequenceIdBuilder builder) => new(builder.Value);
    }
}