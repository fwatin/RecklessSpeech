using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Import;

public record SequencesImportRequestedEvent
(
    string HtmlContent,
    AudioFileNameWithExtension AudioFileNameWithExtension,
    string Tags
) : IDomainEvent;

public record AudioFileNameWithExtension
{
    public string Value { get; }

    public AudioFileNameWithExtension(string value)
    {
        this.Value = value;
    }

    public static AudioFileNameWithExtension Create(string value)
    {
        if (value.EndsWith(".mp3") is false)
            throw new InvalidAudioFileFormatException();

        else return new AudioFileNameWithExtension(value);
    }

    public static AudioFileNameWithExtension Hydrate(string value) => new(value);
}

public class InvalidAudioFileFormatException : Exception
{
}