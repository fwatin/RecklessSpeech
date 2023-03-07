using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using System.Collections.Generic;
using System.Linq;

namespace RecklessSpeech.Web.ViewModels.Sequences
{
    public static class SequenceSummaryQueryModelExtensions
    {
        internal static IReadOnlyCollection<SequenceSummaryPresentation> ToPresentation(
            this IReadOnlyCollection<SequenceSummaryQueryModel> queryModels) =>
            queryModels.Select(x => x.ToPresentation()).ToList();

        internal static SequenceSummaryPresentation ToPresentation(
            this SequenceSummaryQueryModel queryModel) =>
            new(
                queryModel.Id,
                queryModel.Word,
                queryModel.Explanation);
    }
}