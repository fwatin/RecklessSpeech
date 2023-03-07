using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences
{
    public interface
        ISequencesDbContext //todo renommer avec autre nom que sequence genre recklessspeech si au debut une seule db
    {
        List<SequenceEntity> Sequences { get; }
        List<ExplanationEntity> Explanations { get; }
        List<LanguageDictionaryEntity> LanguageDictionaries { get; set; }
    }
}