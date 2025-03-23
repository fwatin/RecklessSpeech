namespace RecklessSpeech.Domain.Questioner;

public sealed class Completion(string value)
{
    public string Value { get; set; } = value;
}