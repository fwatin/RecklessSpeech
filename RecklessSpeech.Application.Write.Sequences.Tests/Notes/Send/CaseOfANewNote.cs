using RecklessSpeech.Application.Write.Sequences.Commands.Notes.SendToAnki;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.Send
{
    public class CaseOfANewNote
    {
        private readonly SendNoteToAnkiCommand command;
        private readonly Guid sequenceId;
        private readonly InMemorySequenceRepository sequenceRepository;
        private readonly SpyNoteGateway spyGateway;
        private readonly SendNoteToAnkiCommandHandler sut;

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
                Explanations = new() { ExplanationBuilder.Create() }
            };
            this.sequenceRepository.Add(sequenceBuilder);

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
                Explanations = new() { ExplanationBuilder.Create() }
            };
            this.sequenceRepository.Add(sequenceBuilder);

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
                TranslatedWord = new("pain"), Explanations = new() { ExplanationBuilder.Create() }
            };
            this.sequenceRepository.Add(sequenceBuilder);

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
                TranslatedSentence = new("des trucs")
            };
            this.sequenceRepository.Add(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.After.Value.Trim().Should().Be("translated sentence from Netflix: \"des trucs\"");
        }

        [Fact]
        public async Task Should_contains_source_from_explanation()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(this.sequenceId) with
            {
                Explanations = new()
                {
                    ExplanationBuilder.Create() with { SourceUrl = new("www.farfelu.com/translation") }
                }
            };
            this.sequenceRepository.Add(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.Source.Value.Should()
                .Contain("<a href=\"www.farfelu.com/translation\">www.farfelu.com/translation</a>");
        }

        [Fact]
        public async Task Should_contains_audio()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(this.sequenceId) with
            {
                AudioFileNameWithExtension = new("368468486.mp3"),
                Explanations = new() { ExplanationBuilder.Create() }
            };
            this.sequenceRepository.Add(sequenceBuilder);

            //Act
            await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.Audio.Value.Should().Be("[sound:368468486.mp3]");
        }
    }
}