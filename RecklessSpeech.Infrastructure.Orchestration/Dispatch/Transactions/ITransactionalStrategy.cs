namespace RecklessSpeech.Infrastructure.Orchestration.Dispatch.Transactions;

public interface ITransactionalStrategy
{
    Task ExecuteTransactional(Func<Task> function);
}