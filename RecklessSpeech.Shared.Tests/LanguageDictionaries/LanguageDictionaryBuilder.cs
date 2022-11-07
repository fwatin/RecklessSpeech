using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Shared.Tests.LanguageDictionaries;

public record LanguageDictionaryBuilder(Guid Id)
{
    public static LanguageDictionaryBuilder Create(Guid id)
    {
        return new LanguageDictionaryBuilder(id);
    }
    public LanguageDictionaryEntity BuildEntity()
    {
        return new LanguageDictionaryEntity()
        {
            Id = this.Id
        };
    }
}