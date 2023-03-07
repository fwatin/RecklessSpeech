using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes
{
    public class AnswerBuilder
    {
        public AnswerBuilder() { }

        public AnswerBuilder(string value) => this.Value = value;

        public string Value { get; set; } = "astuces";


        public static implicit operator Answer(AnswerBuilder builder) => new(builder.Value);
    }
}