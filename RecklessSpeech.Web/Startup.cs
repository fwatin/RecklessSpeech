using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecklessSpeech.Application.Read;
using RecklessSpeech.Application.Write.Sequences;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Orchestration;
using RecklessSpeech.Infrastructure.Read;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Web;

public class Startup
{
    private readonly IConfiguration configuration;
    private readonly IHostEnvironment environment;

    public Startup(IConfiguration configuration,IHostEnvironment environment)
    {
        this.configuration = configuration;
        this.environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddWebDependencies(this.configuration,this.environment)
            .AddSequencePorts()
            .AddSequencesCommands()
            .AddInfrastructure()
            .AddReadPorts()
            .AddReadQueries()
            .AddInfrastructureOrchestration();
    }

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