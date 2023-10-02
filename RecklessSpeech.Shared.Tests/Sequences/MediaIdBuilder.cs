using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class MediaBuilder
    {
        public MediaBuilder() { }

        public MediaBuilder(long id) => this.Id = id;

        public long Id { get; init; } = 4351348384;

        public static implicit operator Media?(MediaBuilder? builder)
        {
            if (builder is null)
            {
                return null;
            }

            return Media.Create(builder.Id, null, null, null);
        }
    }
}