using RecklessSpeech.Application.Write.Sequences.Commands.Notes.ReverseNote;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Shared.Tests.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.ReverseNote
{
    public class CaseOfSuccessful
    {
        private readonly SpyNoteGateway spyNoteGateway;
        private readonly ReverseNoteCommandHandler sut;
        private readonly SequenceBuilder sequenceBuilder;

        public CaseOfSuccessful()
        {
            HtmlContentBuilder htmlContentBuilder = new ();
            this.sequenceBuilder = SequenceBuilder.Create() with
            {
                HtmlContent = new(htmlContentBuilder.Value.Replace("gimmicks","broer"))
            };

            this.spyNoteGateway = new();
            this.sut = new(this.spyNoteGateway);
        }
        
        [Fact]
        public async Task Should_Answer_be_the_former_word()
        {
            //Arrange
            var sequence = this.sequenceBuilder.BuildDomain();
            var note = Note.CreateFromSequence(sequence);
            ReverseNoteCommand command = new(note);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.spyNoteGateway.Note!.Answer.Value.Should().Be("broer");
        }
        
        [Fact]
        public async Task Should_Question_not_contains_gimmicks_anymore()
        {
            //Arrange
            var sequence = this.sequenceBuilder.BuildDomain();
            var note = Note.CreateFromSequence(sequence);
            ReverseNoteCommand command = new(note);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.spyNoteGateway.Note!.Question.Value.Should().NotContain("broer");
        }
    }
}