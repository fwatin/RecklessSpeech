using ExCSS;
using HtmlAgilityPack;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import;
using RecklessSpeech.Domain.Sequences;
using RecklessSpeech.Shared.Tests;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Import
{
    public class CaseOfImportSuccessful
    {
        private readonly SequenceBuilder builder;
        private readonly ImportSequencesCommandHandler sut;

        public CaseOfImportSuccessful()
        {
            this.sut = new(new InMemoryTestSequenceRepository());
            this.builder = SequenceBuilder.Create(Guid.Parse("259FD4F4-082E-46CB-BF1A-94F99780D2E2")) with
            {
                TranslatedWord = null
            };
        }

        [Fact]
        public async Task Should_add_a_new_sequence()
        {
            //Arrange
            ImportSequencesCommand command = this.builder.BuildImportCommand();

            //Act
            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

            //Assert
            events.Should().HaveCount(1);
            (events.First() as ImportedSequenceEvent)!.HtmlContent.Value.Should().NotBeNullOrEmpty();
            (events.First() as ImportedSequenceEvent)!.AudioFileNameWithExtension.Value.Should()
                .NotBeNullOrEmpty();
            (events.First() as ImportedSequenceEvent)!.Tags.Value.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Should_add_a_known_sequence()
        {
            //Arrange
            ImportSequencesCommand command = this.builder.BuildImportCommand();

            //Act
            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

            //Assert
            Fixture.VerifyEquivalence(this.builder.BuildEvent(), (ImportedSequenceEvent)events.First());
        }

        [Fact]
        public async Task Should_add_two_sequences()
        {
            //Arrange
            ImportSequencesCommand command = new(Fixture.SomeContentWithTwoSequences);

            //Act
            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

            //Assert
            events.Should().HaveCount(2);
        }

        [Theory]
        [InlineData(".dc-card")]
        [InlineData(".card")]
        [InlineData(".nightMode .dc-card")]
        [InlineData(".nightMode.card")]
        public async Task Should_html_not_specify_background_styles(string styleName)
        {
            //Arrange
            ImportSequencesCommand importSequencesCommand = this.builder.BuildImportCommand();

            //Act
            IReadOnlyCollection<IDomainEvent>
                events = await this.sut.Handle(importSequencesCommand, CancellationToken.None);

            //Assert
            ImportedSequenceEvent importEvent = (ImportedSequenceEvent)events.First();
            IStyleRule? dcCard = await Fixture.GetStyleRule(importEvent.HtmlContent.Value, styleName);
            dcCard.Should().BeNull();
        }

        [Fact]
        public async Task Should_get_word_in_sequence()
        {
            //Arrange
            ImportSequencesCommand command = this.builder.BuildImportCommand();

            //Act
            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

            //Assert
            ImportedSequenceEvent importEvent = (ImportedSequenceEvent)events.First();
            importEvent.Word.Value.Should().Be("gimmicks");
        }

        [Fact]
        public async Task Should_get_translated_sentence_in_sequence()
        {
            //Arrange
            ImportSequencesCommand command = this.builder.BuildImportCommand();

            //Act
            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

            //Assert
            ImportedSequenceEvent importEvent = (ImportedSequenceEvent)events.First();
            importEvent.TranslatedSentence.Value.Should().Be("Et ça n'arrive pas par quelques astuces statistiques.");
        }

        [Fact]
        public async Task Should_html_not_contain_any_c1_for_flag()
        {
            //Arrange
            ImportSequencesCommand command = this.builder.BuildImportCommand();

            //Act
            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

            //Assert
            ImportedSequenceEvent importEvent = (ImportedSequenceEvent)events.First();
            importEvent.HtmlContent.Value.Should().NotContain("c1::");
        }

        [Fact]
        public async Task Should_show_word_in_red_background_in_html()
        {
            //Arrange
            ImportSequencesCommand importSequencesCommand = this.builder.BuildImportCommand();

            //Act
            IReadOnlyCollection<IDomainEvent>
                events = await this.sut.Handle(importSequencesCommand, CancellationToken.None);

            //Assert
            ImportedSequenceEvent importEvent = (ImportedSequenceEvent)events.First();
            Fixture.VerifyWordHasAttributeBackgroundInRed(importEvent.HtmlContent.Value);
        }

        [Fact]
        public async Task Should_html_not_contain_the_translated_sentence()
        {
            //Arrange
            ImportSequencesCommand command = this.builder.BuildImportCommand();

            //Act
            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

            //Assert
            ImportedSequenceEvent importEvent = (ImportedSequenceEvent)events.First();
            importEvent.HtmlContent.Value.Should().NotContain(this.builder.TranslatedSentence.Value);
        }


        private static class Fixture
        {
            public const string SomeContentWithTwoSequences =
                "\"<style>a lot of things in html\"	[sound:1658501397855.mp3]	\"word-naked lang-nl netflix Green pron \"" +
                "\"<style>a lot of other things in html\"	[sound:123456.mp3]	\"some other tags \"";

            public static async Task<IStyleRule?> GetStyleRule(string htmlContent, string styleName)
            {
                HtmlDocument htmlDoc = new();
                htmlDoc.LoadHtml(htmlContent);
                HtmlNode? styleNode = htmlDoc.DocumentNode.SelectSingleNode("style");
                StylesheetParser parser = new();
                Stylesheet? stylesheet = await parser.ParseAsync(styleNode.InnerText);
                return stylesheet.StyleRules.FirstOrDefault(rule => rule.SelectorText == styleName);
            }

            public static void VerifyEquivalence(ImportedSequenceEvent expected, ImportedSequenceEvent result)
            {
                string exp = expected.HtmlContent.Value.WithoutSpaceAndReturns();
                string res = result.HtmlContent.Value.WithoutSpaceAndReturns();

                result.Should().Match(s => StringCompare.WithoutSpaceAndReturns(exp) == res);

                expected.Should().BeEquivalentTo(result, AssertExtensions.IgnoreIdAndHtmlContent);
            }

            public static void VerifyWordHasAttributeBackgroundInRed(string htmlContent)
            {
                HtmlDocument htmlDoc = new();
                htmlDoc.LoadHtml(htmlContent);
                HtmlNodeCollection? nodes = htmlDoc.DocumentNode.SelectNodes("//span[@class='" + "dc-gap" + "']");
                HtmlNode? dcgapNode = nodes.Single();
                HtmlNode? wordNode = dcgapNode.ChildNodes.Single();
                wordNode.Attributes.Single(x => x.Name == "style").Value.Should()
                    .Be("background-color: rgb(157, 0, 0);");
            }
        }
    }
}