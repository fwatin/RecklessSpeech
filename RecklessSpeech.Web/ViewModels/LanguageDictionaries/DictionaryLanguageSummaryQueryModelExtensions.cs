using System.Collections.Generic;
using System.Linq;
using RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll;

namespace RecklessSpeech.Web.ViewModels.LanguageDictionaries;

public static class DictionaryLanguageSummaryQueryModelExtensions
{
    internal static IReadOnlyCollection<LanguageDictionarySummaryPresentation> ToPresentation(
        this IReadOnlyCollection<LanguageDictionarySummaryQueryModel> queryModels)
    {
        return queryModels.Select(x => x.ToPresentation()).ToList();
    }

    private static LanguageDictionarySummaryPresentation ToPresentation(
        this LanguageDictionarySummaryQueryModel queryModel)
    {
        return new(
            queryModel.Id,
            queryModel.Url,
            queryModel.Name,
            queryModel.FromLanguage,
            queryModel.ToLanguage
            );
    }
}