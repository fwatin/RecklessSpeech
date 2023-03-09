using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Infrastructure.Sequences.Repositories;

namespace RecklessSpeech.Infrastructure.Databases
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
            services
                .AddInMemoryDbContext()
                .AddRepositories();

        private static IServiceCollection AddInMemoryDbContext(this IServiceCollection services)
        {
            InMemoryDataContext inMemoryDataContext = new();

            return services
                .AddSingleton<IDataContext>(inMemoryDataContext)
                .AddSingleton(inMemoryDataContext);
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsExecutorManager, DomainEventsExecutorManager>();

            services.AddScoped<ISequenceRepository, InMemorySequenceRepository>();
            services.AddScoped<InMemorySequenceRepository>();

            services.AddScoped<IExplanationRepository, InMemoryExplanationRepository>();
            services.AddScoped<InMemoryExplanationRepository>();


            return services;
        }
        
        private static IServiceCollection AddEventsExecutor(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsExecutorManager, DomainEventsExecutorManager>();
            return services;
        }

       
    }
}