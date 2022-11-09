namespace RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll;

public record LanguageDictionarySummaryQueryModel(
    Guid Id,
    string Url,
    string Name,
    string FromLanguage,
    string ToLanguage);