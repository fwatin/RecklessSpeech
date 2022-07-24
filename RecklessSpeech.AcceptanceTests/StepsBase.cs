using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.AcceptanceTests.Configuration.Clients;
using RecklessSpeech.Infrastructure.Databases;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests;

public class StepsBase
{
    protected readonly RecklessSpeechDbContext dbContext;
    protected readonly ScenarioContext Context;
    protected ITestsClient Client { get; }
    protected TestsServer TestServer => this.Context.Get<TestsServer>();
    private IServiceProvider ServiceProvider => this.TestServer.ServiceProvider;

    public StepsBase(ScenarioContext context)
    {
        Context = context;
        this.Client = this.TestServer.ServiceProvider.GetRequiredService<ITestsClient>();
        this.dbContext = this.GetService<RecklessSpeechDbContext>();
    }

    private T GetService<T>() where T : notnull
    {
        return this.ServiceProvider.GetRequiredService<T>();
    }
}