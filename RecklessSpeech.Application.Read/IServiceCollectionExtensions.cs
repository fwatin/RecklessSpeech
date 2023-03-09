using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RecklessSpeech.Application.Read
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddReadQueries(this IServiceCollection services) =>
            services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}