using MediatR;

namespace RecklessSpeech.Application.Core.Queries
{
    public abstract class QueryHandler<T, TResult> : IRequestHandler<T, TResult> where T : IRequest<TResult>
    {
        public Task<TResult> Handle(T query, CancellationToken cancellationToken) => this.Handle(query);
        protected abstract Task<TResult> Handle(T query);
    }
}