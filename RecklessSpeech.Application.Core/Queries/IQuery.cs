using MediatR;

namespace RecklessSpeech.Application.Core.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}