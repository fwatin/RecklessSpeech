using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecklessSpeech.Front.WPF.ViewModels
{
    public class HttpBackEndGateway
    {
        private readonly IBackEndGatewayAccess access;
        private const string ApiVersion = "v1";

        public HttpBackEndGateway(IBackEndGatewayAccess access)
        {
            this.access = access;

        }

        public async Task ImportSequencesFromCsvFile(string filePath)
        {
            using MultipartFormDataContent content = new();

            FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);

            content.Add(new StreamContent(fileStream), "file", Path.GetFileName(filePath));

            const string url = @$"https://localhost:47973/api/{ApiVersion}/sequences"; //todo mettre dans des settings

            await this.access.PostAsync(url, content);
        }
        public async Task<IReadOnlyCollection<SequenceDto>> GetAllSequences()
        {
            const string url = @$"https://localhost:47973/api/{ApiVersion}/sequences";

            HttpResponseMessage responseMessage = await this.access.GetAsync(url);

            string contentString = await responseMessage.Content.ReadAsStringAsync();

            IReadOnlyCollection<SequenceSummaryPresentation> result =
                JsonConvert.DeserializeObject<IReadOnlyCollection<SequenceSummaryPresentation>>(contentString);

            return result.Select(presentation => new SequenceDto()
            {
                Id = presentation.Id,
                Word = presentation.Word,
                Explanation = presentation.Explanation
            })
                .ToList();
        }

        public async Task<SequenceDto> GetOneSequence(Guid id)
        {
            string url = @$"https://localhost:47973/api/{ApiVersion}/sequences/{id}";

            HttpResponseMessage? responseMessage = await this.access.GetAsync(url);

            string contentString = await responseMessage.Content.ReadAsStringAsync();

            SequenceSummaryPresentation result =
                JsonConvert.DeserializeObject<SequenceSummaryPresentation>(contentString);

            return new SequenceDto()
            {
                Id = result.Id,
                Word = result.Word,
                Explanation = result.Explanation
            };
        }

        public async Task EnrichSequenceDutch(Guid id)
        {
            const string url = @$"https://localhost:47973/api/{ApiVersion}/sequences/Dictionary/dutch";

            HttpRequestMessage request = BuildJsonMessage(HttpMethod.Post, url, new List<Guid>() { id });

            await this.access.SendAsync(request);
        }

        public async Task EnrichSequenceEnglish(Guid id)
        {
            const string url = @$"https://localhost:47973/api/{ApiVersion}/sequences/Dictionary/english";

            HttpRequestMessage request = BuildJsonMessage(HttpMethod.Post, url, new List<Guid>() { id });

            await this.access.SendAsync(request);
        }


        public async Task SendSequenceToAnki(Guid id)
        {
            const string url = @$"https://localhost:47973/api/{ApiVersion}/sequences/Anki";

            HttpRequestMessage request = BuildJsonMessage(HttpMethod.Post, url, new List<Guid>() { id });

            await this.access.SendAsync(request);
        }

        private static HttpRequestMessage BuildJsonMessage(HttpMethod method, string path, object? parameters)
        {
            HttpRequestMessage request = new(method, new Uri(path, UriKind.RelativeOrAbsolute));
            if (parameters != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(parameters),
                    Encoding.UTF8,
                    "application/json");
            }

            return request;
        }
    }
}
