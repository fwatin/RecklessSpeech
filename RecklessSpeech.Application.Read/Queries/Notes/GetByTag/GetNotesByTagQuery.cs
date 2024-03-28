using MediatR;
using RecklessSpeech.Application.Read.Queries.Notes.Services;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Read.Queries.Notes.GetByTag
{
    public record GetNotesWithBlueFlagQuery : IRequest<IReadOnlyCollection<Note>>;

    public class GetNotesByTagQueryHandler : IRequestHandler<GetNotesWithBlueFlagQuery, IReadOnlyCollection<Note>>
    {
        private readonly IReadNoteGateway noteGateway;

        public GetNotesByTagQueryHandler(IReadNoteGateway noteGateway)
        {
            this.noteGateway = noteGateway;
        }
        
        public async Task<IReadOnlyCollection<Note>> Handle(GetNotesWithBlueFlagQuery request,
            CancellationToken cancellationToken)
        {
            return await noteGateway.GetByFlag(4);
        }
    }
}