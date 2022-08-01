using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record SendNotesCommand(IReadOnlyCollection<Guid> ids) : IEventDrivenCommand;

public class SendNotesCommandHandler : CommandHandlerBase<SendNotesCommand>
{
    private readonly INoteGateway noteGateway;
    private readonly ISequenceQueryRepository sequenceQueryRepository;

    public SendNotesCommandHandler(INoteGateway noteGateway, ISequenceQueryRepository sequenceQueryRepository)
    {
        this.noteGateway = noteGateway;
        this.sequenceQueryRepository = sequenceQueryRepository;
    }

    protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(SendNotesCommand command)
    {
        var notes = new List<NoteDto>();

        foreach (Guid id in command.ids)
        {
            var sequence = sequenceQueryRepository.TryGetOne(id);

            //todo use create 
            var note = Note.Hydrate(new(Guid.NewGuid()), Question.Create(sequence!.HtmlContent));

            notes.Add(note.GetDto());
        }

        await this.noteGateway.Send(notes);

        return await Task.FromResult(Array.Empty<IDomainEvent>());
    }
}