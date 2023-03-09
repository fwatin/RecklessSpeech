using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Read.Queries.Sequences.GetOne;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences.Repositories;

namespace RecklessSpeech.Infrastructure.Read
{
    public class InMemorySequenceQueryRepository : ISequenceQueryRepository
    {
        private readonly IDataContext dbContext;

        public InMemorySequenceQueryRepository(IDataContext dbContext) => this.dbContext = dbContext;

        public async Task<IReadOnlyCollection<SequenceSummaryQueryModel>> GetAll()
        {
            List<SequenceSummaryQueryModel> result = (from entity in this.dbContext.Sequences
                let explanation = this.dbContext.Explanations.SingleOrDefault(x => x.Id == entity.ExplanationId)
                select new SequenceSummaryQueryModel(entity.Id,
                    entity.HtmlContent,
                    entity.AudioFileNameWithExtension,
                    entity.Tags,
                    entity.Word,
                    explanation?.Content)).ToList();

            return await Task.FromResult(result);
        }

        public async Task<SequenceSummaryQueryModel> GetOne(Guid id) =>
            await this.TryGetOne(id) ??
            throw new SequenceNotFoundReadException(id);

        private async Task<SequenceSummaryQueryModel?> TryGetOne(Guid id)
        {
            SequenceDao? entity = this.dbContext.Sequences.FirstOrDefault(x => x.Id == id);
            if (entity is null)
            {
                return null;
            }

            ExplanationDao? explanation = null;
            if (entity.ExplanationId is not null)
            {
                explanation = this.dbContext.Explanations.Single(x => x.Id == entity.ExplanationId);
            }

            SequenceSummaryQueryModel result = new(
                entity.Id,
                entity.HtmlContent,
                entity.AudioFileNameWithExtension,
                entity.Tags,
                entity.Word,
                explanation?.Content);

            return await Task.FromResult(result);
        }
    }
}