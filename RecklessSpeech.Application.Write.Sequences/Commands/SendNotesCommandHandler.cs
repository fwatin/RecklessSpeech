using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record SendNotesCommand(IReadOnlyCollection<Guid> ids) : IEventDrivenCommand;

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
        List<NoteDto> notes = new();

        foreach (Guid id in command.ids)
        {
            Sequence sequence = await this.sequenceRepository.GetOne(id);

            Note note = Note.Create(new(Guid.NewGuid()), Question.Create(sequence!.HtmlContent.Value));

            notes.Add(note.GetDto());
        }

        await this.noteGateway.Send(notes);

        return await Task.FromResult(Array.Empty<IDomainEvent>());
    }
}