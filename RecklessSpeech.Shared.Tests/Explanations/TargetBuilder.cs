using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Shared.Tests.Explanations
{
    public class TargetBuilder
    {
        public TargetBuilder() { }

        public TargetBuilder(string value) => this.Value = value;

        public string Value { get; init; } = "gimmicks";

        public static implicit operator Target(TargetBuilder builder) => new(builder.Value);
    }
}