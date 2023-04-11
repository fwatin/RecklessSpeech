using RecklessSpeech.AcceptanceTests.Configuration.Clients;
using RecklessSpeech.Web.ViewModels.Sequences;
using System.Text;

namespace RecklessSpeech.AcceptanceTests.Configuration
{
    public class SequenceRequestsLatest
    {
        private readonly string basePath;
        private readonly ITestsClient client;

        public SequenceRequestsLatest(ITestsClient client, string apiVersion)
        {
            this.client = client;
            this.basePath = $"/api/{apiVersion}/sequences";
        }

        public async Task ImportSequences(string fileContent, string fileName)
        {
            using MultipartFormDataContent content = new();
            content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(fileContent))), "file", fileName);
            await this.client.Post<IReadOnlyCollection<SequenceSummaryPresentation>>($"http://localhost{this.basePath}",
                content);
        }

        public async Task<IReadOnlyCollection<SequenceSummaryPresentation>> ImportSequencesFromZip(string filePath)
        {
            using MultipartFormDataContent content = CreateMultipartFormDataContent(filePath);
            
            return await this.client.Post<IReadOnlyCollection<SequenceSummaryPresentation>>(
                $"http://localhost{this.basePath}/import-zip", content);
        }

        private static MultipartFormDataContent CreateMultipartFormDataContent(string filePath)
        {
            var multipartFormDataContent = new MultipartFormDataContent();

            byte[] fileContent;
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    fileContent = memoryStream.ToArray();
                }
            }

            ByteArrayContent byteArrayContent = new(fileContent);
            byteArrayContent.Headers.ContentType = new("application/octet-stream");
            byteArrayContent.Headers.ContentDisposition = new("form-data")
            {
                Name = "lln_anki_items_2023-4-11_update_676746.zip",
                FileName = Path.GetFileName(filePath)
            };
            multipartFormDataContent.Add(byteArrayContent, "myFile", Path.GetFileName(filePath));

            return multipartFormDataContent;
        }


        public Task<IReadOnlyCollection<SequenceSummaryPresentation>> GetAll()
            => this.client.Get<IReadOnlyCollection<SequenceSummaryPresentation>>($"http://localhost{this.basePath}");

        public Task<SequenceSummaryPresentation> GetOne(Guid sequenceId)
            => this.client.Get<SequenceSummaryPresentation>($"http://localhost{this.basePath}/{sequenceId}");

        public async Task SendToAnki(Guid id) =>
            await this.client.Post<string>($"http://localhost{this.basePath}/send-to-anki?id={id}");

        public async Task Enrich(Guid sequenceId) =>
            await this.client.Post<string>($"http://localhost{this.basePath}/Dictionary/dutch?id={sequenceId}");
    }
}