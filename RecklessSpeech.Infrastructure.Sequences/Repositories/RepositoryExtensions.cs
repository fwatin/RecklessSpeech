using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
            services
                .AddRepositories();

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            InMemorySequenceRepository sequenceRepository = new();
            services.AddSingleton<ISequenceRepository>(sequenceRepository);
            services.AddSingleton(sequenceRepository);

            services.AddScoped<IMediaRepository, FileMediaRepository>();

            return services;
        }
    }
}