using RecklessSpeech.Domain.Questioner;

namespace RecklessSpeech.Application.Write.Questioner.Ports
{
    public interface IReadNoteGateway
    {
        Task<IReadOnlyCollection<Note>> GetBySubject(string subject);
    }
}