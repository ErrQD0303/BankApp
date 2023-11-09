using Bank.Entities.Contracts;
using Bank.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;

namespace Bank.Entities
{
    /// <summary>
    /// Represents Account of Customer
    /// </summary>
    /// 
    [Serializable]
    public class Account : IAccount
    {
        #region Private Fields
        private Guid _accountID;
        private long _accountNumber;
        private long _customerCode;
        private double _balance;

        #endregion
        #region Public Properties
        /// <summary>
        /// Guid of Account for Unique Identification
        /// </summary>
        public Guid AccountID
        {
            get => _accountID;
            set => _accountID = value;
        }
        /// <summary>
        /// Auto-generated code number of the customer
        /// </summary>
        public long AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (value >= 0)
                {
                    _accountNumber = value;
                }
                else
                {
                    throw new AccountException("Account Number should be positive only.");
                }
            }
        }
        /// <summary>
        /// Customer Code
        /// </summary>
        public long CustomerCode
        {
            get => _customerCode;
            set
            {
                if (value >= 0)
                {
                    _customerCode = value;
                }
                else
                {
                    throw new AccountException("Customer Code should be positive only.");
                }
            }
        }
        /// <summary>
        /// Balance of the account
        /// </summary>
        public double Balance
        {
            get => _balance;
            set
            {
                if (value >= 0)
                {
                    _balance = value;
                }
                else
                {
                    throw new AccountException("Balance should be greater than or equal to 0!");
                }
            }
        }

        #endregion

        #region Methods
        public object Clone()
        {
            return new Account()
            {
                AccountID = this.AccountID,
                AccountNumber = this.AccountNumber,
                CustomerCode = this.CustomerCode,
                Balance = this.Balance
            };
        }
        #endregion
    }
}
