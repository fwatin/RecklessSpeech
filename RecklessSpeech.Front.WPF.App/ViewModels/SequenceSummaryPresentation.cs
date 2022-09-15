using System;

namespace RecklessSpeech.Front.WPF.App.ViewModels;

public record SequenceSummaryPresentation(
    Guid Id,
    string Word,
    string? Explanation);