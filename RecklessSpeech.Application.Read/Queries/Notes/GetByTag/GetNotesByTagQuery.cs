using MediatR;
using RecklessSpeech.Application.Read.Queries.Notes.Services;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Read.Queries.Notes.GetByTag
{
    public record GetNotesToBeReversedQuery : IRequest<IReadOnlyCollection<Note>>;

    public class GetNotesByTagQueryHandler : IRequestHandler<GetNotesToBeReversedQuery, IReadOnlyCollection<Note>>
    {
        private readonly IReadNoteGateway noteGateway;

        public GetNotesByTagQueryHandler(IReadNoteGateway noteGateway)
        {
            this.noteGateway = noteGateway;
        }
        
        public async Task<IReadOnlyCollection<Note>> Handle(GetNotesToBeReversedQuery request,
            CancellationToken cancellationToken)
        {
            return await noteGateway.GetByFlagAndWithoutTag(4, "reversed");
        }
    }
}