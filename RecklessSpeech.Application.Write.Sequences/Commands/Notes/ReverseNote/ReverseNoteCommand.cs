using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Notes.ReverseNote
{
    public record ReverseNoteCommand(Note Note) : IRequest<ReverseNoteResult>;

    public record ReverseNoteResult(bool HasBeenReversed, string Word);

    public class ReverseNoteCommandHandler : IRequestHandler<ReverseNoteCommand, ReverseNoteResult>
    {
        private readonly INoteGateway noteGateway;

        public ReverseNoteCommandHandler(INoteGateway noteGateway)
        {
            this.noteGateway = noteGateway;
        }

        public async Task<ReverseNoteResult> Handle(ReverseNoteCommand request, CancellationToken cancellationToken)
        {
            if (request.Note.IsAlreadyReversed()) return new(false, request.Note.GetDto().Answer.Value);

            var reversedNote = request.Note.CreateReversedNote();
            NoteDto dto = reversedNote.GetDto();
            await this.noteGateway.Send(dto);
            await this.noteGateway.AddTag(request.Note, "reversed");

            return new(true, dto.Answer.Value);
        }
    }
}