using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Notes.Send
{
    public record SendNotesCommand(Guid Id) : IEventDrivenCommand; //todo rename

    public class SendNotesCommandHandler : CommandHandlerBase<SendNotesCommand>
    {
        private readonly INoteGateway noteGateway;
        private readonly ISequenceRepository sequenceRepository;

        public SendNotesCommandHandler(INoteGateway noteGateway, ISequenceRepository sequenceRepository)
        {
            this.noteGateway = noteGateway;
            this.sequenceRepository = sequenceRepository;
        }

        protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(SendNotesCommand command)
        {
            Sequence? sequence = await this.sequenceRepository.GetOne(command.Id);
            if (sequence is null) return ArraySegment<IDomainEvent>.Empty;

            Note note = Note.CreateFromSequence(sequence);

            await this.noteGateway.Send(note.GetDto());

            return await Task.FromResult(Array.Empty<IDomainEvent>());
        }
    }
}