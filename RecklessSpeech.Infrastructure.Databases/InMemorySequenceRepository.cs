using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Explanations;
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

    public async Task<Sequence?> GetOne(Guid id)
    {
        SequenceEntity? entity = this.dbContext.Sequences.SingleOrDefault(x => x.Id == id);
        if (entity is null) return null;

        Explanation? explanation = default;
        if (entity.ExplanationId is not null)
        {
            ExplanationEntity explanationEntity = this.dbContext.Explanations.Single(x => x.Id == entity.ExplanationId);
            
            explanation = Explanation.Hydrate(
                explanationEntity.Id,
                explanationEntity.Content,
                explanationEntity.Target,
                explanationEntity.SourceUrl);
        }

        Sequence sequence = Sequence.Hydrate(
            entity.Id,
            entity.HtmlContent,
            entity.AudioFileNameWithExtension,
            entity.Tags,
            entity.Word,
            entity.TranslatedSentence,
            explanation
        );

        return await Task.FromResult(sequence);
    }
}