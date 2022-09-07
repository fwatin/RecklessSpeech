namespace RecklessSpeech.Domain.Sequences.Notes;

public record Question(string Value)
{
    public static Question Create(string value) //todo passer un value type html content
    {
        return new Question(value);
    }
}