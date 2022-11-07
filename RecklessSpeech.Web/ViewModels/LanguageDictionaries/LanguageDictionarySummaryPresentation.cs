using System;

namespace RecklessSpeech.Web.ViewModels.LanguageDictionaries;

public record LanguageDictionarySummaryPresentation(
    Guid Id,
    string Url,
    string Name,
    string FromLanguage,
    string ToLanguage);