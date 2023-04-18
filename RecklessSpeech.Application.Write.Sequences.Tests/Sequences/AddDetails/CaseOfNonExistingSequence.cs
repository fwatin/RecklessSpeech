using RecklessSpeech.Infrastructure.Sequences.Repositories;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.AddDetails
{
    public class CaseOfNonExistingSequence
    {
        private readonly AddDetailsToSequencesCommandHandler sut;
        private readonly InMemorySequenceRepository inMemorySequenceRepository = new();

        public CaseOfNonExistingSequence() => this.sut = new(this.inMemorySequenceRepository);

        [Theory]
        [InlineData("brood", "pain")]
        [InlineData("ham", "jambon")]
        public async Task Should_do_nothing_if_not_found(string word, string translation)
        {
            //Arrange
            Class1[] dtos = { new() { word = new() { text = word }, wordTranslationsArr = new[] { translation } } };
            AddDetailsToSequencesCommand command = new(dtos);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.inMemorySequenceRepository.All.Should().BeEmpty();
        }
    }
}