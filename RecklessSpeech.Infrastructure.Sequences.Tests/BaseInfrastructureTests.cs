using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Infrastructure.Sequences.Executors;
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
                new RepositoryExecutor(this.InMemoryDataContext)
            });
        }

        protected InMemoryDataContext InMemoryDataContext { get; }
        protected DomainEventsExecutorManager Sut { get; }
    }
}