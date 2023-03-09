using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public static class RepositoryExtensions
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
            services.AddScoped<ISequenceRepository, InMemorySequenceRepository>();
            services.AddScoped<InMemorySequenceRepository>();

            services.AddScoped<IExplanationRepository, InMemoryExplanationRepository>();
            services.AddScoped<InMemoryExplanationRepository>();

            return services;
        }
    }
}