﻿using RecklessSpeech.Application.Core.Queries;
using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Read.Queries.Sequences.GetOne;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetOneSequenceQuery(SequenceId SequenceId) : IQuery<SequenceSummaryQueryModel>;

public class GetOneSequencesQueryHandler : QueryHandler<GetOneSequenceQuery, SequenceSummaryQueryModel>
{
    private readonly ISequenceQueryRepository sequenceQueryRepository;

    public GetOneSequencesQueryHandler(ISequenceQueryRepository sequenceQueryRepository)
    {
        this.sequenceQueryRepository = sequenceQueryRepository;
    }

    protected override async Task<SequenceSummaryQueryModel> Handle(GetOneSequenceQuery query)
    {
        SequenceSummaryQueryModel? sequenceSummaryQueryModel = await this.sequenceQueryRepository.TryGetOne(query.SequenceId.Value);
        if (sequenceSummaryQueryModel is null) throw new SequenceNotFoundReadException(query.SequenceId.Value);
        return sequenceSummaryQueryModel!;

    }
}