using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class TagsBuilder
    {
        public TagsBuilder() { }

        public TagsBuilder(List<string> value) => this.Values = value;

        public List<string> Values { get; init; } = new() { "word-naked", "lang-en", "netflix", "Green noun" };
        public string Value => string.Join(' ', this.Values);
        public static implicit operator List<Tag>(TagsBuilder builder) => builder.Values.Select(Tag.Hydrate).ToList();
    }
}