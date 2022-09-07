using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
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
        IReadOnlyCollection<SequenceSummaryQueryModel> result = dbContext.Sequences.Select(
                entity => new SequenceSummaryQueryModel(
                    entity.Id,
                    entity.HtmlContent,
                    entity.AudioFileNameWithExtension,
                    entity.Tags,
                    entity.Explanation))
            .ToList();

        return await Task.FromResult(result);
    }

    public SequenceSummaryQueryModel? TryGetOne(Guid id)
    {
        var entity = dbContext.Sequences.FirstOrDefault(x => x.Id == id);
        if (entity is null) return null;

        return new SequenceSummaryQueryModel(
            entity.Id,
            entity.HtmlContent,
            entity.AudioFileNameWithExtension,
            entity.Tags,
            entity.Explanation);
    }
}