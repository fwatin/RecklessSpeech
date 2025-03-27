namespace RecklessSpeech.Application.Write.Questioner.Ports
{
    public interface IChatGptGateway
    {
        Task<IReadOnlyList<string>> GetInterests();
    }
}