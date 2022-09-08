using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.AcceptanceTests.Configuration.Clients;
using RecklessSpeech.AcceptanceTests.Extensions;
using RecklessSpeech.Web;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests;

public class TestsServer : IDisposable
{
    private readonly ScenarioContext context;
    private readonly TestServer testServer;
    private bool isDisposed;

    public IServiceProvider ServiceProvider { get; }

    public TestsServer(ScenarioContext context)
    {
        this.context = context;
        this.testServer = this.Initialize();
        this.ServiceProvider = this.testServer.Host.Services;
    }

    private TestServer Initialize()
    {
        IWebHostBuilder builder = WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .UseEnvironment("acceptancetest")
            .ConfigureServices(
                (ctx, services) => { this.ConfigureAcceptanceTests(services); }
            )
            .ConfigureTestServices(services => services
                .SubstituteNoteGateway()
                .SubstituteMijnwoordenboekGatewayAccess())
            ;

        return new TestServer(builder);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
        ;
    }

    private void ConfigureAcceptanceTests(IServiceCollection services)
    {
        services.AddScoped<ITestsClient>(p => new AcceptanceClient(CreateClient(p), this.context, p));
    }

    private static HttpClient CreateClient(IServiceProvider p)
    {
        return ((TestServer) p.GetRequiredService<IServer>()).CreateClient();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing || this.isDisposed) return;
        this.testServer.Dispose();
        this.isDisposed = true;
    }
}