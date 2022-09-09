using System.Collections.Generic;
using System.Linq;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Web.ViewModels.Sequences;

namespace RecklessSpeech.Web.Sequences;

public static class SequenceSummaryQueryModelExtensions
{
    internal static IReadOnlyCollection<SequenceSummaryPresentation> ToPresentation(
        this IReadOnlyCollection<SequenceSummaryQueryModel> queryModels)
    {
        return queryModels.Select(x => x.ToPresentation()).ToList();
    }

    private static SequenceSummaryPresentation ToPresentation(
        this SequenceSummaryQueryModel queryModel)
    {
        return new SequenceSummaryPresentation(
            queryModel.Id,
            queryModel.HtmlContent, 
            queryModel.AudioFileNameWithExtension,
            queryModel.Tags,
            queryModel.Word,
            queryModel.Explanation);
    }
}