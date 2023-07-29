using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public class FileMediaRepository : IMediaRepository
    {
        public async Task SaveInMediaCollection(string commandEntryFullName, byte[] commandContent)
        {
            string mediaFolderPath = GetPath();
            if (string.IsNullOrEmpty(mediaFolderPath)) return;

            string filePath = Path.Combine(mediaFolderPath, commandEntryFullName);

            if (File.Exists(filePath)) return;
            
            await File.WriteAllBytesAsync(filePath,commandContent);
        }

        private static string GetPath()
        {
            const string officeDesktop = @"C:/Users/felix/AppData/Roaming/Anki2/Felix/collection.media";
            const string surfacePro = @"C:\Users\felix\AppData\Roaming\Anki2\Félix\collection.media";
            

            if (Directory.Exists(surfacePro))
            {
                return surfacePro;
            }
            return Directory.Exists(officeDesktop)
                ? officeDesktop
                : string.Empty;
        }

    }
}