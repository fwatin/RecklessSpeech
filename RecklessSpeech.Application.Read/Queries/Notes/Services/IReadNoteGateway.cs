using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Read.Queries.Notes.Services
{
    public interface IReadNoteGateway
    {
        Task<IReadOnlyCollection<Note>> GetByFlag(int flagNumber);
    }
}