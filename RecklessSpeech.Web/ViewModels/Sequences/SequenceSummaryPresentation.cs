using System;

namespace RecklessSpeech.Web.ViewModels.Sequences;

public record SequenceSummaryPresentation(
    Guid Id,
    string HtmlContent,
    string AudioFileNameWithExtension,
    string Tags,
    string? Explanation);