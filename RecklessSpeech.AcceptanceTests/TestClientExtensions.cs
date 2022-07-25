using RecklessSpeech.AcceptanceTests.Configuration.Clients;

namespace RecklessSpeech.AcceptanceTests;

public static class TestClientExtensions
{
    private const string LatestApiVersion = "v1";
    public static TestsClientRequestsLatest Latest(this ITestsClient client) => new(client, LatestApiVersion);
}