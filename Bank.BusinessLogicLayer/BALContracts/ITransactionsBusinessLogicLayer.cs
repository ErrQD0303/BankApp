using BankApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BALContracts
{
    /// <summary>
    /// interface that represents transactions business logic
    /// </summary>
    public interface ITransactionsBusinessLogicLayer
    {
        /// <summary>
        /// Returns all existing transaction
        /// </summary>
        /// <returns>List of Transactions</returns>
        List<ITransaction> GetTransactions();
        /// <summary>
        /// Returns a set of transactions that matches with specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression that contains condition to check</param>
        /// <returns>The list of matching Transactions</returns>
        List<ITransaction> GetTransactionsByCondition(Predicate<ITransaction> predicate);
        /// <summary>
        /// Adds a new Transaction to the existing   Transaction List
        /// </summary>
        /// <param name="transaction">The Transaction object to add</param>
        /// <returns>Return Guid, that indicates the Transaction is added successfully</returns>
        Guid AddTransaction(ITransaction transaction);
    }
}
