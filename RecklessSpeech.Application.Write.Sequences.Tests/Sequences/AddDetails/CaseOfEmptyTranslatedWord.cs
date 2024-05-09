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
                Word = new(word),
                TranslatedWord = null,
                Explanations = new() { ExplanationBuilder.Create() },
                Media = new(0)
            };
            this.sequenceRepository.Add(sequenceBuilder);
            Class1[] dtos = { new() { word = new() { text = word }, wordTranslationsArr = new[] { translation } } };
            AddDetailsToSequencesCommand command = new(dtos);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Arrange
            WordSequence sequence = (WordSequence)this.sequenceRepository.All.Single();
            sequence.TranslatedWord!.Value.Should().Be(translation);
        }
    }
}