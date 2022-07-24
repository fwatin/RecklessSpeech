using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Configuration.Clients;

public class AcceptanceClient : TestClientBase, ITestsClient
{
    private readonly IServiceProvider serviceProvider;

    public AcceptanceClient(HttpClient client, ScenarioContext context, IServiceProvider serviceProvider)
        : base(context, client)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task Initialize()
    {
        return Task.CompletedTask;
    }
}