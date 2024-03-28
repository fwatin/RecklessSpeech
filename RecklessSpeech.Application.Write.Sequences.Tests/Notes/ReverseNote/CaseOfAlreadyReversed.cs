using RecklessSpeech.Application.Write.Sequences.Commands.Notes.ReverseNote;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Shared.Tests.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.ReverseNote
{
    public class CaseOfAlreadyReversed
    {
        private readonly SpyNoteGateway spyNoteGateway;
        private readonly ReverseNoteCommandHandler sut;

        public CaseOfAlreadyReversed()
        {
            this.spyNoteGateway = new();
            this.sut = new(this.spyNoteGateway);
        }
        
        [Fact]
        public async Task Should_Not_be_added_if_already_reversed()
        {
            //Arrange
            var noteBuilder = NoteBuilder.Create(Guid.NewGuid()) with
            {
                Tags = new(new(){"reversed"})
            };
            var note = noteBuilder.BuildAggregate();
            ReverseNoteCommand command = new(note);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.spyNoteGateway.Note.Should().BeNull();
        }
    }
}