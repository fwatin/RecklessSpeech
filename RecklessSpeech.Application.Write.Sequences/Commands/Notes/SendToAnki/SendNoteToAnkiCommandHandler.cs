﻿using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Notes.SendToAnki
{
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
            Sequence? sequence = this.sequenceRepository.GetOne(command.Id);
            if (sequence is null)
            {
                return Unit.Value;
            }

            Note note = Note.CreateFromSequence(sequence);

            await this.noteGateway.Send(note.GetDto());
            return Unit.Value;
        }
    }
}