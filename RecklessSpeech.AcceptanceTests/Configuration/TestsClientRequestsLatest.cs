using RecklessSpeech.AcceptanceTests.Configuration.Clients;

namespace RecklessSpeech.AcceptanceTests.Configuration
{
    public class TestsClientRequestsLatest
    {
        private readonly string apiVersion;
        private readonly ITestsClient client;

        public TestsClientRequestsLatest(ITestsClient client, string apiVersion)
        {
            this.client = client;
            this.apiVersion = apiVersion;
        }

        public SequenceRequestsLatest SequenceRequests() => new(this.client, this.apiVersion);
    }
}