using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Read.Queries.Notes.Services
{
    public interface IReadNoteGateway
    {
        Task<IReadOnlyCollection<Note>> GetByFlagAndWithoutTag(int flagNumber,string missingTag);
    }
}