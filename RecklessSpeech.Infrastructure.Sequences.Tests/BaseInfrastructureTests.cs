using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Infrastructure.Sequences.Repositories;

namespace RecklessSpeech.Infrastructure.Sequences.Tests
{
    public class BaseInfrastructureTests
    {
        protected BaseInfrastructureTests()
        {
            this.InMemoryDataContext = new();
            this.Sut = new(new IDomainEventExecutor[]
            {
                new SequenceDomainEventRepository(this.InMemoryDataContext)
            });
        }

        protected InMemoryDataContext InMemoryDataContext { get; }
        protected DomainEventsExecutor Sut { get; }
    }
}