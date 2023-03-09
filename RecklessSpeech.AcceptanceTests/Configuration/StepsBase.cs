using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.AcceptanceTests.Configuration.Clients;
using RecklessSpeech.Infrastructure.Sequences.Repositories;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Configuration
{
    public class StepsBase
    {
        protected readonly ScenarioContext Context;

        public StepsBase(ScenarioContext context)
        {
            this.Context = context;
            this.Client = this.TestServer.ServiceProvider.GetRequiredService<ITestsClient>();
            this.DbContext = this.GetService<IDataContext>();
        }

        protected IDataContext DbContext { get; }
        protected ITestsClient Client { get; }
        protected TestsServer TestServer => this.Context.Get<TestsServer>();
        private IServiceProvider ServiceProvider => this.TestServer.ServiceProvider;

        protected T GetService<T>() where T : notnull => this.ServiceProvider.GetRequiredService<T>();

        protected IDataContext GetDbContext() => this.ServiceProvider.GetRequiredService<IDataContext>();
    }
}