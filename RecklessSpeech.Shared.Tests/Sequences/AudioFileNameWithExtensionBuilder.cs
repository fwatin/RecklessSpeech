using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class AudioFileNameWithExtensionBuilder
    {
        public AudioFileNameWithExtensionBuilder()
        {
        }

        public AudioFileNameWithExtensionBuilder(string value) => this.Value = value;

        public string Value { get; set; } = "1658501397855.mp3";

        public static implicit operator AudioFileNameWithExtension(AudioFileNameWithExtensionBuilder builder) =>
            new(builder.Value);
    }
}