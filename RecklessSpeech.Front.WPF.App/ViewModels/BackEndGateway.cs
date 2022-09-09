using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecklessSpeech.Front.WPF.App.ViewModels
{
    public class BackEndGateway
    {
        private const string apiVersion = "v1";

        public static async Task ImportSequencesFromCsvFile(string filePath)
        {
            using HttpClient client = new();

            using MultipartFormDataContent content = new();

            FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);

            content.Add(new StreamContent(fileStream), "file", "fileName_what_for");

            const string url = @$"https://localhost:47973/api/{apiVersion}/sequences";

            await client.PostAsync(new Uri(url), content);
        }

        public static async Task<IReadOnlyCollection<SequenceDto>> GetAllSequences()
        {
            using HttpClient client = new();

            const string url = @$"https://localhost:47973/api/{apiVersion}/sequences";

            HttpResponseMessage? responseMessage = await client.GetAsync(new Uri(url));

            string contentString = await responseMessage.Content.ReadAsStringAsync();

            IReadOnlyCollection<SequenceSummaryPresentation> result = JsonConvert.DeserializeObject<IReadOnlyCollection<SequenceSummaryPresentation>>(contentString);

            return result.Select(presentation => new SequenceDto()
            {
                Id = presentation.Id,
                Word = presentation.Word
            }).ToList();
        }
        
        // ReSharper disable once ClassNeverInstantiated.Local
        private record SequenceSummaryPresentation(
            Guid Id,
            string HtmlContent,
            string AudioFileNameWithExtension,
            string Tags,
            string Word,
            string? Explanation);
    }


    
    
}
