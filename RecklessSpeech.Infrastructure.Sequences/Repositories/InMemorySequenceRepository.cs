using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public class InMemorySequenceRepository : ISequenceRepository
    {
        private readonly IDataContext dbContext;

        public InMemorySequenceRepository(IDataContext dbContext) => this.dbContext = dbContext;

        public async Task<Sequence?> GetOne(Guid id)
        {
            SequenceDao? entity = this.dbContext.Sequences.SingleOrDefault(x => x.Id == id);
            if (entity is null)
            {
                return null;
            }

            return await this.CreateSequenceFromEntity(entity);
        }

        public async Task<Sequence?> GetOneByWord(string word)
        {
            SequenceDao? entity = this.dbContext.Sequences.SingleOrDefault(x => x.Word == word);
            if (entity is null)
            {
                return null;
            }

            return await this.CreateSequenceFromEntity(entity);
        }

        private async Task<Sequence> CreateSequenceFromEntity(SequenceDao dao)
        {
            Explanation? explanation = default;
            if (dao.ExplanationId is not null)
            {
                ExplanationDao explanationDao =
                    this.dbContext.Explanations.Single(x => x.Id == dao.ExplanationId);

                explanation = Explanation.Hydrate(
                    explanationDao.Id,
                    explanationDao.Content,
                    explanationDao.Target,
                    explanationDao.SourceUrl);
            }

            Sequence sequence = Sequence.Hydrate(
                dao.Id,
                dao.HtmlContent,
                dao.AudioFileNameWithExtension,
                dao.Tags,
                dao.Word,
                dao.TranslatedSentence,
                explanation,
                dao.TranslatedWord
            );

            return await Task.FromResult(sequence);
        }
    }
}