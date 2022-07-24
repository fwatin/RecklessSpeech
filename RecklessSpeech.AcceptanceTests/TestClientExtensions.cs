using RecklessSpeech.AcceptanceTests.Configuration.Clients;

namespace RecklessSpeech.AcceptanceTests;

public static class TestClientExtensions
{
    private const string LatestApiVersion = "v0";
    public static TestsClientRequestsLatests Latest(this ITestsClient client) => new(client, LatestApiVersion);
}