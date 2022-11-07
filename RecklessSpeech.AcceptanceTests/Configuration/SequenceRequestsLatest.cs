using System.Text;
using RecklessSpeech.AcceptanceTests.Configuration.Clients;
using RecklessSpeech.Web.ViewModels.Sequences;

namespace RecklessSpeech.AcceptanceTests.Configuration;

public class SequenceRequestsLatest
{
    private readonly ITestsClient client;
    private readonly string basePath;

    public SequenceRequestsLatest(ITestsClient client, string apiVersion)
    {
        this.client = client;
        this.basePath = $"/api/{apiVersion}/sequences";
    }

    public async Task ImportSequences(string fileContent, string fileName)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(fileContent))), "file", fileName);
        await this.client.Post<string>($"http://localhost{this.basePath}", content);
    }

    public Task<IReadOnlyCollection<SequenceSummaryPresentation>> GetAll()
        => this.client.Get<IReadOnlyCollection<SequenceSummaryPresentation>>($"http://localhost{this.basePath}");

    public Task<SequenceSummaryPresentation> GetOne(Guid sequenceId)
        => this.client.Get<SequenceSummaryPresentation>($"http://localhost{this.basePath}/{sequenceId}");

    public async Task SendToAnki(List<Guid> ids)
    {
        await this.client.Post<string>($"http://localhost{this.basePath}/Anki", ids);
    }

    public async Task Enrich(Guid sequenceId)
    {
        await this.client.Post<string>($"http://localhost{this.basePath}/Dictionary", new List<Guid>() {sequenceId});
    }
    
    public async Task AssignLanguageDictionary(Guid id, Guid dictionaryId)
    {
        await this.client.Put<string>($"http://localhost{this.basePath}/Dictionary/{id}", dictionaryId);
    }
}