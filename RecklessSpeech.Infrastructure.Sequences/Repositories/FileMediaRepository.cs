using RecklessSpeech.Application.Write.Sequences.Ports;
using System;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public class FileMediaRepository : IMediaRepository
    {
        public async Task SaveInMediaCollection(string commandEntryFullName, byte[] commandContent)
        {
            const string mediaFolderPath = @"C:/Users/felix/AppData/Roaming/Anki2/Félix/collection.media";

            if (!Directory.Exists(mediaFolderPath))
            {
                return;
            }

            string filePath = Path.Combine(mediaFolderPath, commandEntryFullName);
            
            await File.WriteAllBytesAsync(filePath,commandContent);
        }

    }
}