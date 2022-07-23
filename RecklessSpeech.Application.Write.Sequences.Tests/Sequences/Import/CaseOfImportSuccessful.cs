using System.Text;
using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences;
using RecklessSpeech.Domain.Shared;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Import;

public class CaseOfImportSuccessful
{
    private readonly ImportSequencesCommandHandler sut;

    public CaseOfImportSuccessful()
    {
        this.sut = new ImportSequencesCommandHandler();
    }

    [Fact]
    public async Task Should_add_a_new_sequence()
    {
        //Arrange
        ImportSequencesCommand command = new(Data.SomeContent);

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command);

        //Assert
        events.Should().ContainEquivalentOf(Data.ExpectedEvent);
    }

    [Fact]
    public async Task Should_add_two_sequences()
    {
        //Arrange
        ImportSequencesCommand command = new(Data.SomeContentWithTwoSequences);

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command);

        //Assert
        events.Should().HaveCount(2);
    }

    private static class Data
    {
        public const string SomeHtml = "\"<style>a lot of things in html\"";
        public const string SomeAudiofileName = "1658501397855.mp3";
        public const string SomeTags = "\"word-naked lang-nl netflix Green pron \"";

        public const string SomeContent =
            "\"<style>a lot of things in html\"	[sound:1658501397855.mp3]	\"word-naked lang-nl netflix Green pron \"";

        public const string SomeContentWithTwoSequences =
            "\"<style>a lot of things in html\"	[sound:1658501397855.mp3]	\"word-naked lang-nl netflix Green pron \"" +
            "\"<style>a lot of other things in html\"	[sound:123456.mp3]	\"some other tags \"";
        

        public static readonly SequencesImportRequestedEvent ExpectedEvent =
            new(SomeHtml, AudioFileNameWithExtension.Hydrate(SomeAudiofileName), SomeTags);
    }
}