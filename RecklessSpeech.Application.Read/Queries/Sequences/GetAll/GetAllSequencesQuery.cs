using MediatR;
using RecklessSpeech.Application.Core.Queries;

namespace RecklessSpeech.Application.Read.Queries.Sequences.GetAll
{
    public record GetAllSequencesQuery :  IRequest<IReadOnlyCollection<SequenceSummaryQueryModel>>;
}