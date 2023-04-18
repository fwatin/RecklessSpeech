using MediatR;

namespace RecklessSpeech.Application.Read.Queries.Sequences.GetAll
{
    public record GetAllSequencesQuery :  IRequest<IReadOnlyCollection<SequenceSummaryQueryModel>>;
}