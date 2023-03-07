namespace RecklessSpeech.Domain.Sequences.Sequences;

public record AudioFileNameWithExtension(string Value)
{
    public static AudioFileNameWithExtension Create(string value)
    {
        if (value.EndsWith(".mp3") is false)
            throw new InvalidAudioFileFormatException();

        else return new(value);
    }

    public static AudioFileNameWithExtension Hydrate(string value) => new(value);
}