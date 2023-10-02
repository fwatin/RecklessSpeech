using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories
{
    public class InMemoryMediaRepository : IMediaRepository
    {
        public Task SaveInMediaCollection(string commandEntryFullName, byte[] commandContent)
        {
            throw new NotImplementedException();
        }
    }
}