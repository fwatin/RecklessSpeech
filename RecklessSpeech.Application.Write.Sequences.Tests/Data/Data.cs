namespace RecklessSpeech.Application.Write.Sequences.Tests.Data
{
    public static class Data
    {
        public static string GetFileInDataFolder(string name)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Path.Combine(currentDirectory, "Data",name);
            return File.ReadAllText(path);
        }
    }
}