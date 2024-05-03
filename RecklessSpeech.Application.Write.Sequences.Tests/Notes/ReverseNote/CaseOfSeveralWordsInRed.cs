using RecklessSpeech.Application.Write.Sequences.Commands.Notes.ReverseNote;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.ReverseNote
{
    public class CaseOfSeveralWordsInRed
    {
        private readonly SpyNoteGateway spyNoteGateway;
        private readonly ReverseNoteCommandHandler sut;
        private readonly SequenceBuilder sequenceBuilder;

        public CaseOfSeveralWordsInRed()
        {
            this.spyNoteGateway = new();
            this.sut = new(this.spyNoteGateway);
            string html = Data.Data.GetFileInDataFolder("severalwordsinred_metjou.html");
            this.sequenceBuilder = SequenceBuilder.Create() with
            {
                HtmlContent = new(html),
                TranslatedWord = new("avec toi")
            };
        }

        [Fact]
        public async Task ShouldBeTreatedAsASingleWordWhenAreContiguous()
        {
            //Arrange
            var sequence = this.sequenceBuilder.BuildDomain();
            var note = Note.CreateFromSequence(sequence);
            ReverseNoteCommand command = new(note);
            
            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.spyNoteGateway.Note!.Answer.Value.Should().Be("met jou");
        }
        
        [Fact]
        public async Task QuestionShouldNotContainAnymoreAnyOfTheContiguousWords()
        {
            //Arrange
            var sequence = this.sequenceBuilder.BuildDomain();
            var note = Note.CreateFromSequence(sequence);
            ReverseNoteCommand command = new(note);
            
            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.spyNoteGateway.Note!.Question.Value.Should().NotContain("met");
            this.spyNoteGateway.Note!.Question.Value.Should().NotContain("jou");
        }
        
        [Fact]
        public async Task ShouldNotFindAnythingInRedInQuestionAnymore()
        {
            //Arrange
            var sequence = this.sequenceBuilder.BuildDomain();
            var note = Note.CreateFromSequence(sequence);
            ReverseNoteCommand command = new(note);
            
            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.spyNoteGateway.Note!.Question.Value.Should().NotContain("rgb(157, 0, 0)");
        }
    }
}