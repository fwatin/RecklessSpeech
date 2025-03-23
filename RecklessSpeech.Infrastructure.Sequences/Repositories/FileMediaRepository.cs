using Microsoft.Extensions.Options;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Anki;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public class FileMediaRepository : IMediaRepository
    {
        private readonly IOptions<AnkiSequenceSettings> settings;
        public FileMediaRepository(IOptions<AnkiSequenceSettings> settings)
        {
            this.settings = settings;
        }

        public async Task SaveInMediaCollection(string commandEntryFullName, byte[] commandContent)
        {
            string filePath = Path.Combine(this.settings.Value.MediaPath, commandEntryFullName);
            
            if (File.Exists(filePath)) return;

            await File.WriteAllBytesAsync(filePath, commandContent);
        }
    }
}