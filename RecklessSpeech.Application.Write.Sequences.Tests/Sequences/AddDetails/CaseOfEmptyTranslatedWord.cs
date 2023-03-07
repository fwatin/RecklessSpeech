namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.AddDetails
{
    public class CaseOfEmptyTranslatedWord
    {
        private readonly InMemoryTestSequenceRepository sequenceRepository;
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
            this.sequenceRepository.Feed(sequenceBuilder);
            Class1[] dtos =
            {
                new() { word = new() { text = word }, wordTranslationsArr = new string[1] { translation } }
            };
            AddDetailsToSequencesCommand command = new(dtos);

            //Act
            IReadOnlyCollection<IDomainEvent> ev = await this.sut.Handle(command, CancellationToken.None);

            //Arrange
            SetTranslatedWordEvent expected = new(new(sequenceBuilder.SequenceId.Value), new(translation));
            ev.Single().Should().BeEquivalentTo(expected);
        }
    }
}