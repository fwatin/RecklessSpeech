using System;

namespace RecklessSpeech.Front.WPF.ViewModels
{
    public record SequenceSummaryPresentation(
    Guid Id,
    string Word,
    string? Explanation);
}