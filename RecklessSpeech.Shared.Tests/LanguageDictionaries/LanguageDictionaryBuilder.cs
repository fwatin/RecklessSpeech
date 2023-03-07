using RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Web.ViewModels.LanguageDictionaries;

namespace RecklessSpeech.Shared.Tests.LanguageDictionaries
{
    public record LanguageDictionaryBuilder(
        LanguageDictionaryIdBuilder LanguageDictionaryId,
        UrlBuilder Url,
        NameBuilder Name,
        FromLanguageBuilder FromLanguage,
        ToLanguageBuilder ToLanguage)
    {
        public static LanguageDictionaryBuilder Create(Guid id) =>
            new(
                new(id),
                new(),
                new(),
                new(),
                new()
            );

        public LanguageDictionaryEntity BuildEntity() =>
            new()
            {
                Id = this.LanguageDictionaryId.Value,
                Url = this.Url.Value,
                Name = this.Name.Value,
                FromLanguage = this.FromLanguage.Value,
                ToLanguage = this.ToLanguage.Value
            };

        public LanguageDictionarySummaryQueryModel BuildQueryModel() =>
            new(
                this.LanguageDictionaryId.Value,
                this.Url.Value,
                this.Name.Value,
                this.FromLanguage.Value,
                this.ToLanguage.Value
            );

        public LanguageDictionarySummaryPresentation BuildSummaryPresentation() =>
            new(
                this.LanguageDictionaryId.Value,
                this.Url.Value,
                this.Name.Value,
                this.FromLanguage.Value,
                this.ToLanguage.Value
            );
    }
}