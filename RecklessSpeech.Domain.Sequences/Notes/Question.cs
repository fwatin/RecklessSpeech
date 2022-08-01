using System.Runtime.CompilerServices;

namespace RecklessSpeech.Domain.Sequences.Notes;

public record Question(string Value)
{
    public static Question Create(string value)
    {
        return new Question(value);
    }
}