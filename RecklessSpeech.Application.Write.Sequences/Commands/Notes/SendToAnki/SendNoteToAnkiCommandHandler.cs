using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Notes.SendToAnki
{
    public class SendNoteToAnkiCommandHandler : CommandHandlerBase<SendNoteToAnkiCommand>
    {
        private readonly INoteGateway noteGateway;
        private readonly ISequenceRepository sequenceRepository;

        public SendNoteToAnkiCommandHandler(INoteGateway noteGateway, ISequenceRepository sequenceRepository)
        {
            this.noteGateway = noteGateway;
            this.sequenceRepository = sequenceRepository;
        }

        protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(SendNoteToAnkiCommand command)
        {
            Sequence? sequence = this.sequenceRepository.GetOne(command.Id);
            if (sequence is null)
            {
                return ArraySegment<IDomainEvent>.Empty;
            }

            Note note = Note.CreateFromSequence(sequence);

            await this.noteGateway.Send(note.GetDto());

            return await Task.FromResult(Array.Empty<IDomainEvent>());
        }
    }
}