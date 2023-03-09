using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Databases
{
    public class InMemoryExplanationRepository : IExplanationRepository
    {
        private readonly IDataContext dbContext;

        public InMemoryExplanationRepository(IDataContext dbContext) => this.dbContext = dbContext;


        public Explanation? TryGetByTarget(string target)
        {
            ExplanationDao? entity = this.dbContext.Explanations.SingleOrDefault(x => x.Target == target);

            return entity is null
                ? null
                : Explanation.Hydrate(entity.Id, entity.Content, entity.Target, entity.SourceUrl);
        }
    }
}