using RecklessSpeech.AcceptanceTests.Configuration.Clients;

namespace RecklessSpeech.AcceptanceTests.Configuration;

public class TestsClientRequestsLatest
{
    private readonly ITestsClient client;
    private readonly string apiVersion;

    public TestsClientRequestsLatest(ITestsClient client, string apiVersion)
    {
        this.client = client;
        this.apiVersion = apiVersion;
    }

    public SequenceRequestsLatest SequenceRequests() => new(this.client, this.apiVersion);
}