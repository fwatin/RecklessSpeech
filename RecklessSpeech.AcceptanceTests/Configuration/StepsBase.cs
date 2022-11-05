using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.AcceptanceTests.Configuration.Clients;
using RecklessSpeech.Infrastructure.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests;

public class StepsBase
{
    protected readonly ScenarioContext Context;
    protected ITestsClient Client { get; }
    protected TestsServer TestServer => this.Context.Get<TestsServer>();
    private IServiceProvider ServiceProvider => this.TestServer.ServiceProvider;

    public StepsBase(ScenarioContext context)
    {
        this.Context = context;
        this.Client = this.TestServer.ServiceProvider.GetRequiredService<ITestsClient>();
    }

    protected T GetService<T>() where T : notnull
    {
        return this.ServiceProvider.GetRequiredService<T>();
    }
    
    protected ISequencesDbContext GetDbContext()
    {
        return this.ServiceProvider.GetRequiredService<ISequencesDbContext>();
    }
}