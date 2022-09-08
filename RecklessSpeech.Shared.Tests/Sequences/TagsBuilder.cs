using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public class TagsBuilder
{
    public TagsBuilder() { }
    public TagsBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; set; } = "word-naked lang-nl netflix Green pron";

    public static implicit operator Tags(TagsBuilder builder) => new(builder.Value);
}