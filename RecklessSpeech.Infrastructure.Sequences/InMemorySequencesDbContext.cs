using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences
{
    public class InMemoryDataContext : IDataContext
    {
        public InMemoryDataContext()
        {
            this.Explanations = new();
            this.Sequences = new();
            this.LanguageDictionaries = new();

            this.Initialise();
        }

        public List<SequenceEntity> Sequences { get; }
        public List<ExplanationEntity> Explanations { get; }
        public List<LanguageDictionaryEntity> LanguageDictionaries { get; set; }

        private void Initialise() =>
            this.LanguageDictionaries.Add(new()
            {
                Id = Guid.Parse("1224B241-1368-4AF6-B88B-DFA65E8CD232"),
                Url = $"https://www.wordreference.com/enfr/{1}",
                Name = "WordReference",
                FromLanguage = "English",
                ToLanguage = "French"
            });
    }
}