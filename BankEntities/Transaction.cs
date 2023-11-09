using BankApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    /// <summary>
    /// class represents   Transaction
    /// </summary>
    [Serializable]
    public class Transaction : ITransaction
    {
        #region Private Fields
        Guid _TransactionID;
        private DateTime _transactionDate;
        private long _sourceAccountNumber;
        private long _destinationAccountNumber;
        private double _amount;
        private TransactionType _transactionType;

        #endregion
        #region Public Properties
        /// <summary>
        /// Transaction ID
        /// </summary>
        public Guid TransactionID { get => _TransactionID; set => _TransactionID = value; }
        /// <summary>
        /// Transaction Date
        /// </summary>
        public DateTime TransactionDate { get => _transactionDate; set => _transactionDate = value; }
        /// <summary>
        /// Source Account Number
        /// </summary>
        public long SourceAccountNumber { get => _sourceAccountNumber; set => _sourceAccountNumber = value; }
        /// <summary>
        /// Destination Account Number
        /// </summary>
        public long DestinationAccountNumber { get => _destinationAccountNumber; set => _destinationAccountNumber = value; }
        /// <summary>
        /// Transaction Amount
        /// </summary>
        public double Amount { get => _amount; set => _amount = value; }
        /// <summary>
        /// Transaction Type
        /// </summary>
        public TransactionType transactionType { get => _transactionType; set => _transactionType = value; }
        #endregion

        #region Methods
        /// <summary>
        /// Clone method
        /// </summary>
        /// <returns>a deep copy of the object</returns>
        public object Clone()
        {
            return new Transaction
            {
                TransactionDate = this.TransactionDate,
                SourceAccountNumber = this.SourceAccountNumber,
                DestinationAccountNumber = this.DestinationAccountNumber,
                Amount = this.Amount
            };
        }
        #endregion
    }
}
