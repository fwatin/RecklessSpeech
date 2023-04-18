using MediatR;
using RecklessSpeech.Application.Read.Ports;

namespace RecklessSpeech.Application.Read.Queries.Sequences.GetAll
{
    public class
        GetAllSequencesQueryHandler : IRequestHandler<GetAllSequencesQuery, IReadOnlyCollection<SequenceSummaryQueryModel>>
    {
        private readonly ISequenceQueryRepository sequenceQueryRepository;

        public GetAllSequencesQueryHandler(ISequenceQueryRepository sequenceQueryRepository) =>
            this.sequenceQueryRepository = sequenceQueryRepository;

        public async Task<IReadOnlyCollection<SequenceSummaryQueryModel>> Handle(GetAllSequencesQuery request, CancellationToken cancellationToken)
        {
            return (await this.sequenceQueryRepository.GetAll()).ToList();
        }
    }
}