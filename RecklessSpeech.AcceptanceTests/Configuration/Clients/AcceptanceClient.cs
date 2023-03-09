using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Configuration.Clients
{
    public class AcceptanceClient : TestClientBase, ITestsClient
    {
        public AcceptanceClient(HttpClient client, ScenarioContext context, IServiceProvider serviceProvider)
            : base(context, client)
        {
        }

        public Task Initialize() => Task.CompletedTask;
    }
}