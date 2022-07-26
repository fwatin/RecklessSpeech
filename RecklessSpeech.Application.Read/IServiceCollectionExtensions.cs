using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RecklessSpeech.Application.Read;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddReadQueries(this IServiceCollection services)
    {
        return services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}