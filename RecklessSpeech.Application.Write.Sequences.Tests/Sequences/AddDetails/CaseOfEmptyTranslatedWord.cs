using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails;
using RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.AddDetails;

public class CaseOfEmptyTranslatedWord
{
    private readonly AddDetailsToSequencesCommandHandler sut;
    private readonly InMemoryTestSequenceRepository sequenceRepository;

    public CaseOfEmptyTranslatedWord()
    {
        sequenceRepository = new();
        this.sut = new(sequenceRepository);
    }

    [Theory]
    [InlineData("brood","pain")]
    [InlineData("ham","jambon")]
    public async Task Should_set_translated_word(string word, string translation)
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create() with { Word = new(word), TranslatedWord = null };
        this.sequenceRepository.Feed(sequenceBuilder);
        Class1[] dtos = { new() { word = new() { text = word }, wordTranslationsArr = new string[1] { translation } } };
        AddDetailsToSequencesCommand command = new(dtos);

        //Act
        var ev=await this.sut.Handle(command, CancellationToken.None);

        //Arrange
        SetTranslatedWordEvent expected = new(new(sequenceBuilder.SequenceId.Value), new(translation));
        ev.Single().Should().BeEquivalentTo(expected);
    }
}