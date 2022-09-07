using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Read;

public class InMemorySequenceQueryRepository : ISequenceQueryRepository
{
    private readonly ISequencesDbContext dbContext;

    public InMemorySequenceQueryRepository(ISequencesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<SequenceSummaryQueryModel>> GetAll()
    {
        //todo faire une jointure
        List<SequenceSummaryQueryModel> result = new();

        foreach (SequenceEntity entity in this.dbContext.Sequences)
        {

            ExplanationEntity? explanation = this.dbContext.Explanations.SingleOrDefault(x => x.Id == entity.ExplanationId);

            var queryModel = new SequenceSummaryQueryModel(
                entity.Id,
                entity.HtmlContent,
                entity.AudioFileNameWithExtension,
                entity.Tags,
                entity.Word,
                explanation?.Value);

            result.Add(queryModel);
        }

        return await Task.FromResult(result);
    }

    public SequenceSummaryQueryModel? TryGetOne(Guid id)
    {
        var entity = this.dbContext.Sequences.FirstOrDefault(x => x.Id == id);
        if (entity is null) return null;

        ExplanationEntity? explanation = null;
        if (entity.ExplanationId is not null)
        {
            explanation = this.dbContext.Explanations.Single(x => x.Id == entity.ExplanationId);
        }

        return new SequenceSummaryQueryModel(
            entity.Id,
            entity.HtmlContent,
            entity.AudioFileNameWithExtension,
            entity.Tags,
            entity.Word,
            explanation?.Value);
    }
}