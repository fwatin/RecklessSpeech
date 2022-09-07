namespace RecklessSpeech.Domain.Sequences.Explanations;

public sealed class Explanation
{
    public string Value { get; set; } //todo mettre un value type
    //todo rename
    public string Word { get; set; } //todo mettre un value type
    public Guid Id { get; init; } //todo mettre un value type

    private Explanation(Guid id, string value, string word)
    {
        this.Id = id;
        this.Value = value;
        this.Word = word;

    }
    public static Explanation Create(string value, string word)
    {
        return new Explanation(Guid.NewGuid(), value, word);
    }
} 