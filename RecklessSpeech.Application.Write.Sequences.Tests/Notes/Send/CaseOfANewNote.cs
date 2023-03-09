using RecklessSpeech.Application.Write.Sequences.Commands.Notes.Send;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.Send
{
    public class CaseOfANewNote
    {
        private readonly SendNotesCommand command;
        private readonly Guid sequenceId;
        private readonly InMemoryTestSequenceRepository sequenceRepository;
        private readonly SpyNoteGateway spyGateway;
        private readonly SendNotesCommandHandler sut;

        public CaseOfANewNote()
        {
            this.spyGateway = new();
            this.sequenceRepository = new();
            this.sut = new(this.spyGateway, this.sequenceRepository);

            this.sequenceId = Guid.Parse("79FAD304-21BC-4B58-BECF-0884016DCC11");
            this.command = new(this.sequenceId);
        }

        [Fact]
        public async Task Should_contain_only_one()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(this.sequenceId) with
            {
                Explanation = ExplanationBuilder.Create()
            };
            this.sequenceRepository.Feed(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_contains_html_in_question()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(this.sequenceId) with
            {
                HtmlContent = new("\"<style> some html here for this test\""),
                Explanation = ExplanationBuilder.Create()
            };
            this.sequenceRepository.Feed(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.Question.Value.Should().Be(sequenceBuilder.HtmlContent.Value);
        }

        [Fact]
        public async Task Should_contains_answer_if_translated_word()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(this.sequenceId) with
            {
                TranslatedWord = new("pain"), Explanation = ExplanationBuilder.Create()
            };
            this.sequenceRepository.Feed(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.Answer.Value.Should().Be("pain");
        }

        [Fact]
        public async Task Should_contains_explanation_in_after()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(this.sequenceId) with
            {
                TranslatedSentence = new("hey this is the translated sentence from Netflix"),
                Explanation = ExplanationBuilder.Create() with { Content = new("a lot of explanations") }
            };
            this.sequenceRepository.Feed(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.After.Value.Trim().Should().Be(
                "translated sentence from Netflix: \"hey this is the translated sentence from Netflix\"a lot of explanations");
        }

        [Fact]
        public async Task Should_contains_source_from_explanation()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(this.sequenceId) with
            {
                Explanation = ExplanationBuilder.Create()with { SourceUrl = new("www.farfelu.com/translation") }
            };
            this.sequenceRepository.Feed(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.Source.Value.Should()
                .Be("<a href=\"www.farfelu.com/translation\">www.farfelu.com/translation</a>");
        }

        [Fact]
        public async Task Should_contains_audio()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(this.sequenceId) with
            {
                AudioFileNameWithExtension = new("368468486.mp3"), Explanation = ExplanationBuilder.Create()
            };
            this.sequenceRepository.Feed(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.Audio.Value.Should().Be("[sound:368468486.mp3]");
        }
    }
}