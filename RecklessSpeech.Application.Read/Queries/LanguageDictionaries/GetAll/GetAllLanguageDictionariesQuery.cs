using RecklessSpeech.Application.Core.Queries;

namespace RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll
{
    public record GetAllLanguageDictionariesQuery : IQuery<IReadOnlyCollection<LanguageDictionarySummaryQueryModel>>;
}