using RecklessSpeech.AcceptanceTests.Configuration.Clients;

namespace RecklessSpeech.AcceptanceTests;

public class TestsClientRequestsLatests
{
    private readonly ITestsClient client;
    private readonly string apiVersion;

    public TestsClientRequestsLatests(ITestsClient client, string apiVersion)
    {
        this.client = client;
        this.apiVersion = apiVersion;
    }

    public SequenceRequestsLatest SequenceRequests() => new(this.client, this.apiVersion);
}