using RecklessSpeech.Application.Core.Queries;
using RecklessSpeech.Application.Read.Ports;

namespace RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll
{
    public class GetAllLanguageDictionariesQueryHandler : QueryHandler<GetAllLanguageDictionariesQuery,
        IReadOnlyCollection<LanguageDictionarySummaryQueryModel>>
    {
        private readonly ILanguageDictionaryQueryRepository languageDictionaryQueryRepository;

        public GetAllLanguageDictionariesQueryHandler(
            ILanguageDictionaryQueryRepository languageDictionaryQueryRepository) =>
            this.languageDictionaryQueryRepository = languageDictionaryQueryRepository;

        protected override async Task<IReadOnlyCollection<LanguageDictionarySummaryQueryModel>>
            Handle(GetAllLanguageDictionariesQuery query) =>
            (await this.languageDictionaryQueryRepository.GetAll()).ToList();
    }
}