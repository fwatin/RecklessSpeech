using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Infrastructure.Sequences.Repositories;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.AddDetails
{
    public class CaseOfEmptyTranslatedWord
    {
        private readonly InMemorySequenceRepository sequenceRepository;
        private readonly AddDetailsToSequencesCommandHandler sut;

        public CaseOfEmptyTranslatedWord()
        {
            this.sequenceRepository = new();
            this.sut = new(this.sequenceRepository);
        }

        [Theory]
        [InlineData("brood", "pain")]
        [InlineData("ham", "jambon")]
        public async Task Should_set_translated_word(string word, string translation)
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create() with
            {
                Word = new(word), TranslatedWord = null, Explanation = ExplanationBuilder.Create()
            };
            this.sequenceRepository.Add(sequenceBuilder);
            Class1[] dtos = { new() { word = new() { text = word }, wordTranslationsArr = new[] { translation } } };
            AddDetailsToSequencesCommand command = new(dtos);

            //Act
            IReadOnlyCollection<IDomainEvent> ev = await this.sut.Handle(command, CancellationToken.None);

            //Arrange
            SetTranslatedWordEvent expected = new(new(sequenceBuilder.SequenceId.Value), new(translation));
            ev.Single().Should().BeEquivalentTo(expected);
        }
    }
}