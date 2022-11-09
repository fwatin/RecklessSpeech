using RecklessSpeech.Application.Core.Queries;
using RecklessSpeech.Application.Read.Ports;

namespace RecklessSpeech.Application.Read.Queries.Sequences.GetAll;

public class GetAllSequencesQueryHandler : QueryHandler<GetAllSequencesQuery, IReadOnlyCollection<SequenceSummaryQueryModel>>
{
    private readonly ISequenceQueryRepository sequenceQueryRepository;

    public GetAllSequencesQueryHandler(ISequenceQueryRepository sequenceQueryRepository)
    {
        this.sequenceQueryRepository = sequenceQueryRepository;
    }

    protected override async Task<IReadOnlyCollection<SequenceSummaryQueryModel>> Handle(GetAllSequencesQuery query)
    {
        return (await this.sequenceQueryRepository.GetAll()).ToList();
    }
}

