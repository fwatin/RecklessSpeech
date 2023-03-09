using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes
{
    public class SourceBuilder
    {
        public SourceBuilder() { }

        public SourceBuilder(string value) => this.Value = value;

        public string Value { get; init; } = "https://www.mijnwoordenboek.nl/vertaal/NL/FR/gimmicks";


        public static implicit operator Source(SourceBuilder builder) => new(builder.Value);
    }

    public class AudioBuilder
    {
        public AudioBuilder() { }

        public AudioBuilder(string value) => this.Value = value;

        public string Value { get; init; } = "[sound:1653366482748.mp3]";


        public static implicit operator Audio(AudioBuilder builder) => new(builder.Value);
    }
}