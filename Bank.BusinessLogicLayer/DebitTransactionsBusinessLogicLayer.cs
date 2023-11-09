using Bank.DataAccessLayer;
using Bank.DataAccessLayer.DALContracts;
using Bank.Exceptions;
using BankApp.Contracts;
using BusinessLogicLayer.BALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    /// <summary>
    /// Represents Transactions business logic
    /// </summary>
    public class TransactionsBusinessLogicLayer : ITransactionsBusinessLogicLayer
    {
        #region Private Fields
        private ITransactionsDataAccessLayer _transactionsDataAccessLayer;

        #endregion
        #region Properties
        private ITransactionsDataAccessLayer transactionsDataAccessLayer { get => _transactionsDataAccessLayer; set => _transactionsDataAccessLayer = value; }

        #endregion
        #region Constructors
        public TransactionsBusinessLogicLayer()
        {
            transactionsDataAccessLayer = new TransactionsDataAccessLayer();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns all existing transaction
        /// </summary>
        /// <returns>List of Transactions</returns>
        public List<ITransaction> GetTransactions()
        {
            try
            {
                //invoke DAL
                return transactionsDataAccessLayer.GetTransactions();
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Returns a set of transactions that matches with specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression that contains condition to check</param>
        /// <returns>The list of matching Transactions</returns>
        public List<ITransaction> GetTransactionsByCondition(Predicate<ITransaction> predicate)
        {
            try
            {
                //invoke DAL
                return transactionsDataAccessLayer.GetTransactionsByCondition(predicate);
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Adds a new Transaction to the existing Transaction List
        /// </summary>
        /// <param name="transaction">The Transaction object to add</param>
        /// <returns>Return Guid, that indicates the Transaction is added successfully</returns>
        public Guid AddTransaction(ITransaction transaction)
        {
            try
            {
                //invoke DAL
                transaction.TransactionDate = DateTime.Now;
                return transactionsDataAccessLayer.AddTransaction(transaction);
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
