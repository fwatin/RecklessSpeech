using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Databases;

public class InMemorySequenceRepository : ISequenceRepository //todo à mettre ailleurs
{
    private readonly List<SequenceEntity> sequences = new();

    public void FeedOne(SequenceEntity entity)
    {
        sequences.Add(entity);
    }

    public async Task<Sequence> GetOne(Guid id)
    {
        var entity = sequences.Single(x => x.Id == id);

        var sequence = Sequence.Create(
            entity.Id,
            HtmlContent.Hydrate(entity.HtmlContent),
            AudioFileNameWithExtension.Hydrate(entity.AudioFileNameWithExtension),
            Tags.Hydrate(entity.Tags),
            Word.Hydrate(entity.Word),
            TranslatedSentence.Hydrate(""));

        return await Task.FromResult(sequence);
    }
}