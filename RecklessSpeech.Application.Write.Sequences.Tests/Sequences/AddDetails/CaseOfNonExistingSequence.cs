namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.AddDetails;

public class CaseOfNonExistingSequence
{
    private readonly AddDetailsToSequencesCommandHandler sut;

    public CaseOfNonExistingSequence()
    {
        this.sut = new(new InMemoryTestSequenceRepository());
    }

    [Theory]
    [InlineData("brood","pain")]
    [InlineData("ham","jambon")]
    public async Task Should_do_nothing_if_not_found(string word, string translation)
    {
        //Arrange
        Class1[] dtos = { new() { word = new() { text = word }, wordTranslationsArr = new string[1] { translation } } };
        AddDetailsToSequencesCommand command = new(dtos);

        //Act
        var ev=await this.sut.Handle(command, CancellationToken.None);

        //Assert
        ev.Should().BeEmpty();
    }
}