using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes
{
    public class QuestionBuilder
    {
        public QuestionBuilder() { }

        public QuestionBuilder(string value) => this.Value = value;

        public string Value { get; set; } = "default value for question";

        public static implicit operator Question(QuestionBuilder builder) => new(builder.Value);
    }
}