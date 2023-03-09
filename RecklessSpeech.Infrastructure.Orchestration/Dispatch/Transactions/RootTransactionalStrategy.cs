using System.Transactions;

namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch.Transactions
{
    public class RootTransactionalStrategy : ITransactionalStrategy
    {
        public async Task ExecuteTransactional(Func<Task> function)
        {
            using TransactionScope scope = new(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);
            await function();
            scope.Complete();
        }
    }
}