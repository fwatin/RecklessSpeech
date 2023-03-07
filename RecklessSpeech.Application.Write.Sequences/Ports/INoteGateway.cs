using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Ports
{
    public interface INoteGateway
    {
        Task Send(IReadOnlyCollection<NoteDto> notes);
    }
}