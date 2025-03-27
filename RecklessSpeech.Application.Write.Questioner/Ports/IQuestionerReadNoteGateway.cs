using RecklessSpeech.Domain.Questioner;

namespace RecklessSpeech.Application.Write.Questioner.Ports
{
    public interface IQuestionerReadNoteGateway
    {
        Task<IReadOnlyCollection<Note>> GetBySubject(string subject);
    }
}