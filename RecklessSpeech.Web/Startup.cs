﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecklessSpeech.Application.Read;
using RecklessSpeech.Application.Write.Sequences;
using RecklessSpeech.Infrastructure.Sequences.Repositories;
using RecklessSpeech.Web.Configuration.Cors;

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
                .AddCorsService()
                .AddWebDependencies(this.configuration, this.environment)
                .AddSequencesCommands()
                .AddInfrastructure()
                .AddSequenceGateways()
                .AddQuestionerGateways()
                .AddReadQueries();

        public void Configure(IApplicationBuilder app, IConfiguration config)
        {
            app.UseHttpsRedirection();
            app.UseExceptionHandler("/error");
            app.UseRouting();
            if (this.configuration.IsSwaggerActive())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowEverything");

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}