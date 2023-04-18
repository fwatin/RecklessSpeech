using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public class FileMediaRepository : IMediaRepository
    {
        public async Task SaveInMediaCollection(string commandEntryFullName, byte[] commandContent)
        {
            const string mediaFolderPath = @"C:/Users/felix/AppData/Roaming/Anki2/Felix/collection.media";
            
            if (!Directory.Exists(mediaFolderPath))
            {
                return;
            }

            string filePath = Path.Combine(mediaFolderPath, commandEntryFullName);

            if (File.Exists(filePath)) return;
            
            await File.WriteAllBytesAsync(filePath,commandContent);
        }

    }
}