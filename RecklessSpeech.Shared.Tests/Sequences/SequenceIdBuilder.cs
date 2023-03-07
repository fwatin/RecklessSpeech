using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class SequenceIdBuilder
    {
        public SequenceIdBuilder(Guid value) => this.Value = value;

        public Guid Value { get; } = Guid.Parse("A2527331-DACE-49AA-8339-0FC9C11ED6AB");

        public static implicit operator SequenceId(SequenceIdBuilder builder) => new(builder.Value);
    }
}