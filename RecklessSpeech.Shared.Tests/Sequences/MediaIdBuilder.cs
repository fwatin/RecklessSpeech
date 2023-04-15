using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class MediaIdBuilder
    {
        public MediaIdBuilder() { }

        public MediaIdBuilder(long value) => this.Value = value;

        public long Value { get; init; } = 4351348384;

        public static implicit operator MediaId?(MediaIdBuilder? builder)
        {
            if (builder is null)
            {
                return null;
            }

            return new(builder.Value);
        }
    }
}