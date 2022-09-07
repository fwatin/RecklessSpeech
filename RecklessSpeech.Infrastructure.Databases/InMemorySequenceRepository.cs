using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Databases;

public class InMemorySequenceRepository : ISequenceRepository
{
    private readonly ISequencesDbContext dbContext;

    public InMemorySequenceRepository(ISequencesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Sequence> GetOne(Guid id)
    {
        SequenceEntity entity = this.dbContext.Sequences.Single(x => x.Id == id);

        Sequence sequence = Sequence.Create(
            entity.Id,
            HtmlContent.Hydrate(entity.HtmlContent),
            AudioFileNameWithExtension.Hydrate(entity.AudioFileNameWithExtension),
            Tags.Hydrate(entity.Tags),
            Word.Hydrate(entity.Word),
            TranslatedSentence.Hydrate(""));

        return await Task.FromResult(sequence);
    }
}