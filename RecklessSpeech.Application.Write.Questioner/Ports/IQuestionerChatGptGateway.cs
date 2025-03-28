using RecklessSpeech.Domain.Questioner;

namespace RecklessSpeech.Application.Write.Questioner.Ports
{
    public interface IQuestionerChatGptGateway
    {
        Task<IReadOnlyList<string>> GetInterests(IReadOnlyCollection<Note> relatedNotes,
            Completion commandCompletion,string subject);
    }
}