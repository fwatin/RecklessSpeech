using ExCSS;
using FluentAssertions;
using HtmlAgilityPack;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Shared.Tests;
using RecklessSpeech.Shared.Tests.Sequences;
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
        ImportSequencesCommand command = SequenceBuilder.Create(Guid.Parse("A04EAFA8-A2D0-4055-BE62-6508CA4555E2"))
            .BuildImportCommand();

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        events.Should().HaveCount(1);
        ((events.First() as SequencesImportRequestedEvent)!).HtmlContent.Value.Should().NotBeNullOrEmpty();
        ((events.First() as SequencesImportRequestedEvent)!).AudioFileNameWithExtension.Value.Should()
            .NotBeNullOrEmpty();
        ((events.First() as SequencesImportRequestedEvent)!).Tags.Value.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Should_add_a_known_sequence()
    {
        //Arrange
        var sequenceBuilder = SequenceBuilder.Create(Guid.Parse("6236FA6E-74DE-4B4A-98A4-A9A0B4BAB71D"));
        ImportSequencesCommand command = new(sequenceBuilder.BuildUnformatedSequence());

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        events.Should().ContainEquivalentOf(sequenceBuilder.BuildEvent(), AssertExtensions.IgnoreId);
    }

    [Fact]
    public async Task Should_add_two_sequences()
    {
        //Arrange
        ImportSequencesCommand command = new(Data.SomeContentWithTwoSequences);

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        events.Should().HaveCount(2);
    }

    [Fact]
    public async Task Should_html_not_contain_style_for_background()
    {
        //Arrange
        ImportSequencesCommand command = new(Some.SomeRealCaseCsvFileContent);

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        SequencesImportRequestedEvent importEvent = (SequencesImportRequestedEvent) events.First();
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(importEvent.HtmlContent.Value);
        var styleNode = htmlDoc.DocumentNode.SelectSingleNode("style");

        var parser = new StylesheetParser();
        var stylesheet = await parser.ParseAsync(styleNode.InnerText);
        var dcCard = stylesheet.StyleRules.First(rule =>
            rule.SelectorText ==".dc-card");

        dcCard.Style.Declarations.Where(property => property.Name == "background-color").Should().BeEmpty();
    }

    private static class Data
    {
        public const string SomeContentWithTwoSequences =
            "\"<style>a lot of things in html\"	[sound:1658501397855.mp3]	\"word-naked lang-nl netflix Green pron \"" +
            "\"<style>a lot of other things in html\"	[sound:123456.mp3]	\"some other tags \"";
    }
}