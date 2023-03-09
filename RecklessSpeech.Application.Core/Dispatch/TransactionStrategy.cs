using System.Transactions;

namespace RecklessSpeech.Application.Core.Dispatch
{
    /// <summary>
    ///     In the code you provided, the TransactionScope is used to enclose the execution of the function()
    ///     that modifies the database. The TransactionScope ensures that all modifications to the database performed
    ///     in the function() are done within the context of a transaction. If an exception is thrown during the
    ///     execution of the function, the transaction will be rolled back and all modifications made to
    ///     the database up until that point will be undone.
    /// </summary>
    public interface ITransactionalStrategy
    {
        Task ExecuteTransactionInReadCommitted(Func<Task> function);
    }

    public class TransactionStrategy : ITransactionalStrategy
    {
        /// <summary>
        ///     The IsolationLevel.ReadCommitted option used in the constructor
        ///     of TransactionOptions specifies the isolation level of the transaction.
        ///     In this case, the isolation level is ReadCommitted, which means that modifications
        ///     to the database made by other transactions will not be visible in this transaction until they are committed.
        ///     This ensures that the transaction is executed in a consistent and isolated manner from other transactions.
        /// </summary>
        public async Task ExecuteTransactionInReadCommitted(Func<Task> function)
        {
            TransactionOptions options = new() { IsolationLevel = IsolationLevel.ReadCommitted };

            using TransactionScope scope = new(TransactionScopeOption.Required, options,
                TransactionScopeAsyncFlowOption.Enabled);

            await function();

            scope.Complete();
        }
    }
}