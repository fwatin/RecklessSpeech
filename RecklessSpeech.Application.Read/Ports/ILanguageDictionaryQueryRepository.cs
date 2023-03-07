using RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll;

namespace RecklessSpeech.Application.Read.Ports
{
    public interface ILanguageDictionaryQueryRepository
    {
        Task<IReadOnlyCollection<LanguageDictionarySummaryQueryModel>> GetAll();
    }
}