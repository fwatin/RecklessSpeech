namespace RecklessSpeech.Application.Write.Sequences.Ports
{
    public interface IMediaRepository
    {
        Task SaveInMediaCollection(string commandEntryFullName, byte[] commandContent);
    }
}