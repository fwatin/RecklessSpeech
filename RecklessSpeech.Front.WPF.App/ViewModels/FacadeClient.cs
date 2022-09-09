using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecklessSpeech.Front.WPF.App.ViewModels
{
    public class FacadeClient //todo renommer en ApplicationGateway
    {
        private const string apiVersion = "v1";

        public async Task Flow_CreateAPI(string filePath)
        {
            using HttpClient client = new();

            using var content = new MultipartFormDataContent();
            
            var fileStream = new FileStream(filePath,FileMode.Open,FileAccess.Read);
            
            content.Add(new StreamContent(fileStream), "file", "fileName_what_for");

            var basePath = $"/api/{apiVersion}/sequences";

            string url = @$"https://localhost:47973/api/{apiVersion}/sequences";

            await client.PostAsync(new Uri(url), content);
        }
    }
}
