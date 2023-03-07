using System.Collections.Generic;
using System.Linq;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;

namespace RecklessSpeech.Web.ViewModels.Sequences;

public static class SequenceSummaryQueryModelExtensions
{
    internal static IReadOnlyCollection<SequenceSummaryPresentation> ToPresentation(
        this IReadOnlyCollection<SequenceSummaryQueryModel> queryModels)
    {
        return queryModels.Select(x => x.ToPresentation()).ToList();
    }

    internal static SequenceSummaryPresentation ToPresentation(
        this SequenceSummaryQueryModel queryModel)
    {
        return new(
            queryModel.Id,
            queryModel.Word,
            queryModel.Explanation);
    }
}