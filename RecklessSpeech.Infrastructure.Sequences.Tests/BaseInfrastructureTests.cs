using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Sequences.Tests
{
    public class BaseInfrastructureTests
    {
        protected BaseInfrastructureTests()
        {
            this.InMemoryDataContext = new();
            this.Sut = new(new IDomainEventRepository[]
            {
                new SequenceDomainEventRepository(this.InMemoryDataContext)
            });
        }

        protected InMemoryDataContext InMemoryDataContext { get; }
        protected DomainEventsRepository Sut { get; }
    }
}