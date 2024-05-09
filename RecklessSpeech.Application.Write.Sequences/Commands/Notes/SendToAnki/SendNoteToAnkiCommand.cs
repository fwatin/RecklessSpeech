using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Notes.SendToAnki
{
    public record SendNoteToAnkiCommand(Guid Id)  : IRequest;
    
    public class SendNoteToAnkiCommandHandler : IRequestHandler<SendNoteToAnkiCommand>
    {
        private readonly INoteGateway noteGateway;
        private readonly ISequenceRepository sequenceRepository;

        public SendNoteToAnkiCommandHandler(INoteGateway noteGateway, ISequenceRepository sequenceRepository)
        {
            this.noteGateway = noteGateway;
            this.sequenceRepository = sequenceRepository;
        }

        public async Task<Unit> Handle(SendNoteToAnkiCommand command, CancellationToken cancellationToken)
        {
            var sequence = this.sequenceRepository.GetOne(command.Id);
            if (sequence is null)
            {
                return Unit.Value;
            }

            Note note = Note.CreateFromSequence(sequence);

            await this.noteGateway.Send(note.GetDto());

            sequence.SentToAnkiTimes++;
            return Unit.Value;
        }
    }

}