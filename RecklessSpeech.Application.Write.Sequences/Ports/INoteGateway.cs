using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Ports
{
    public interface INoteGateway
    {
        Task Send(NoteDto note);
        Task AddTag(Note note, string reversed);
    }
}