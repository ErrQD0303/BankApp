using BankApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Bank.Exceptions;
using Bank.DataAccessLayer.DALContracts;
using BankApp;

namespace Bank.DataAccessLayer
{
    public class TransactionsDataAccessLayer : ITransactionsDataAccessLayer
    {
        #region Fields
        private static List<ITransaction> _transactions;

        #endregion
        #region Properties
        public static List<ITransaction> transactions { get => _transactions; set => _transactions = value; }
        #endregion
        
        #region Constructors
        static TransactionsDataAccessLayer()
        {
            transactions = new List<ITransaction>();
            LoadFromFile();
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
                //create a new Transactions List
                List<ITransaction> transactions = new List<ITransaction>();

                //copy all Transactions from the source collection into the new Transactions list
                TransactionsDataAccessLayer.transactions.ForEach(item => transactions.Add(item));
                return transactions;
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
                //create a new Transaction list
                List<ITransaction> transactions = new List<ITransaction>();

                //filter the collection
                List<ITransaction> filteredTransactions = TransactionsDataAccessLayer.transactions.FindAll(predicate);

                //copy all   Transactions from the source collection into the new   Transactions list
                filteredTransactions.ForEach(item => transactions.Add(item.Clone() as ITransaction));
            
                return filteredTransactions;
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
        /// Adds a new Transaction to the existing   Transaction List
        /// </summary>
        /// <param name="transaction">The Transaction object to add</param>
        /// <returns>Return Guid, that indicates the Transaction is added successfully</returns>
        public Guid AddTransaction(ITransaction transaction)
        {
            try
            {
                //generate new Guid
                transaction.TransactionID = Guid.NewGuid();

                //add   Transaction
                transactions.Add(transaction);

                //write to binary file
                WriteFile();

                return transaction.TransactionID;
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
        /// Load Transactions List from file
        /// </summary>
        private static void LoadFromFile()
        {
            using (FileStream fs = new FileStream(@"C:\Users\ADMIN\source\repos\BankApp\Bank.DataAccessLayer\DataFiles\TransactionsList.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length == 0)
                {
                    return;
                }
                BinaryFormatter bf = new BinaryFormatter();
                List<Transaction> newTransactions = new List<Transaction>();
                newTransactions = bf.Deserialize(fs) as List<Transaction>;
                newTransactions.ForEach(item => transactions.Add(item));
            }
        }
        /// <summary>
        /// Wwrite all Transactions to file
        /// </summary>
        private static void WriteFile()
        {
            using (FileStream fs = new FileStream(@"C:\Users\ADMIN\source\repos\BankApp\Bank.DataAccessLayer\DataFiles\TransactionsList.txt", FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<Transaction> newTransactions = new List<Transaction>();
                transactions.ForEach(item => newTransactions.Add(item as Transaction));
                bf.Serialize(fs, newTransactions);
            }
        }
        #endregion
    }
}
