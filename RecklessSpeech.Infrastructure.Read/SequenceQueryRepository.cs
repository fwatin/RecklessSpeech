﻿using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Read;

public class SequenceQueryRepository : ISequenceQueryRepository
{
    private readonly ISequencesDbContext dbContext;

    public SequenceQueryRepository(ISequencesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<SequenceSummaryQueryModel>> GetAll()
    {
        IReadOnlyCollection<SequenceSummaryQueryModel> result= this.dbContext.Sequences.Select(
            entity=> new SequenceSummaryQueryModel(
                entity.HtmlContent,
                entity.AudioFileNameWithExtension,
                entity.Tags)).ToList();

        return await Task.FromResult(result);
    }
}