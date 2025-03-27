using RecklessSpeech.Domain.Questioner;

namespace RecklessSpeech.Application.Write.Questioner.Ports
{
    public interface IChatGptGateway
    {
        Task<IReadOnlyList<string>> GetInterests(IReadOnlyCollection<Note> relatedNotes,
            Completion commandCompletion);
    }
}