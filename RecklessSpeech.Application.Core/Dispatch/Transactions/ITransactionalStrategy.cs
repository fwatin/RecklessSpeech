namespace RecklessSpeech.Application.Core.Dispatch.Transactions
{
    public interface ITransactionalStrategy
    {
        Task ExecuteTransactional(Func<Task> function);
    }
}