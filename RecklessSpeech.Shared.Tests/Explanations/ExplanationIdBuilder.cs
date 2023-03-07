using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Shared.Tests.Explanations
{
    public class ExplanationIdBuilder
    {
        public ExplanationIdBuilder(Guid value)
        {
            this.Value = value;
        }
        
        public ExplanationIdBuilder()
        {
            this.Value = Guid.NewGuid();
        }

        public Guid Value { get; }

        public static implicit operator ExplanationId(ExplanationIdBuilder builder) => new(builder.Value);
    }
}