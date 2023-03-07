using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Read
{
    public class InMemoryLanguageDictionaryQueryRepository : ILanguageDictionaryQueryRepository
    {
        private readonly IDataContext dbContext;

        public InMemoryLanguageDictionaryQueryRepository(IDataContext dbContext) => this.dbContext = dbContext;

        public async Task<IReadOnlyCollection<LanguageDictionarySummaryQueryModel>> GetAll()
        {
            List<LanguageDictionarySummaryQueryModel> result = this.dbContext.LanguageDictionaries.Select(entity =>
                    new LanguageDictionarySummaryQueryModel(
                        entity.Id,
                        entity.Url,
                        entity.Name,
                        entity.FromLanguage,
                        entity.ToLanguage
                    ))
                .ToList();

            return await Task.FromResult(result);
        }
    }
}