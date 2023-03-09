using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.AcceptanceTests.Configuration.Clients;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Configuration
{
    [Binding]
    public class ScenarioInitializer : IDisposable
    {
        private readonly ScenarioContext context;
        private TestsServer? server;

        public ScenarioInitializer(ScenarioContext context) => this.context = context;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [BeforeScenario]
        public async Task Init()
        {
            this.server = new(this.context);
            this.context.Set(this.server);
            ITestsClient client = this.server.ServiceProvider.GetRequiredService<ITestsClient>();
            await client.Initialize();
        }

        [AfterScenario]
        public void Clean() => this.server?.Dispose();

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.server?.Dispose();
            }
        }
    }
}