using MediatR;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Read.Queries.Sequences.GetOne
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public record GetOneSequenceQuery(SequenceId SequenceId) : IRequest<SequenceSummaryQueryModel>;

    public class GetOneSequencesQueryHandler : IRequestHandler<GetOneSequenceQuery, SequenceSummaryQueryModel>
    {
        private readonly ISequenceQueryRepository sequenceQueryRepository;

        public GetOneSequencesQueryHandler(ISequenceQueryRepository sequenceQueryRepository) =>
            this.sequenceQueryRepository = sequenceQueryRepository;

        public async Task<SequenceSummaryQueryModel> Handle(GetOneSequenceQuery request, CancellationToken cancellationToken)
        {
            SequenceSummaryQueryModel? r = this.sequenceQueryRepository.GetOne(request.SequenceId.Value)?.ToQueryModel();
            if (r is null) throw new($"could not find {request.SequenceId.Value}");
            return await Task.FromResult(r);
        }
    }
}