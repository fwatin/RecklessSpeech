using RecklessSpeech.Application.Write.Sequences.Commands.Notes.ReverseNote;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.ReverseNote
{
    public class CaseOfDifferentReds
    {
        private readonly SpyNoteGateway spyNoteGateway;
        private readonly ReverseNoteCommandHandler sut;
        private readonly SequenceBuilder sequenceBuilder;

        public CaseOfDifferentReds()
        {
            this.spyNoteGateway = new();
            this.sut = new(this.spyNoteGateway);
            string html = Data.Data.GetFileInDataFolder("de_kater.html");
            this.sequenceBuilder = SequenceBuilder.Create() with
            {
                HtmlContent = new(html),
                TranslatedWord = new("la gueule de bois")
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
            this.spyNoteGateway.Note!.Answer.Value.Should().Be("de kater");
        }
    }
}