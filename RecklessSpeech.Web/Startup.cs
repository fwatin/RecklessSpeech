using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecklessSpeech.Application.Core.Dispatch;
using RecklessSpeech.Application.Core.Events.Executor;
using RecklessSpeech.Application.Read;
using RecklessSpeech.Application.Write.Sequences;
using RecklessSpeech.Infrastructure.Read;
using RecklessSpeech.Infrastructure.Sequences.Executors;
using RecklessSpeech.Infrastructure.Sequences.Repositories;

namespace RecklessSpeech.Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostEnvironment environment;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddWebDependencies(this.configuration, this.environment)
                .AddEventsExecutors()
                .AddSequencesCommands()
                .AddEventsExecutor()
                .AddInfrastructure()
                .AddReadPorts()
                .AddReadQueries()
                .AddInfrastructureOrchestration();

        public void Configure(IApplicationBuilder app, IConfiguration config)
        {
            app.UseExceptionHandler("/error");
            app.UseRouting();
            if (this.configuration.IsSwaggerActive())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}