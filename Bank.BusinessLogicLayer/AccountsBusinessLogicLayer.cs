using Bank.Configuration;
using Bank.DataAccessLayer;
using Bank.DataAccessLayer.DALContracts;
using Bank.Exceptions;
using Bank.Entities;
using Bank.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    /// <summary>
    /// Represents Account business logic
    /// </summary>
    public class AccountsBusinessLogicLayer : IAccountsBusinessLogicLayer
    {
        #region Private Fields
        private IAccountsDataAccessLayer _accountDataAccessLayer;
        #endregion

        #region Constructors
        public AccountsBusinessLogicLayer()
        {
            accountsDataAccessLayer = new AccountsDataAccessLayer();
        }

        #endregion

        #region Properties
        public IAccountsDataAccessLayer accountsDataAccessLayer { get => _accountDataAccessLayer; set => _accountDataAccessLayer = value; }

        #endregion

        #region Methods
        /// <summary>
        /// Returns all existing Accounts of Customer
        /// </summary>
        /// <returns>List of Account</returns>
        public List<IAccount> GetAccounts()
        {
            try
            {
                //invoke DAL
                return accountsDataAccessLayer.GetAccounts();
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a set of Accounts that matches with specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression that contains condition to check</param>
        /// <returns>The list of matching Accounts</returns>
        public List<IAccount> GetAccountsByCondition(Predicate<IAccount> predicate)
        {
            try
            {
                //invoke DAL
                return accountsDataAccessLayer.GetAccountsByCondition(predicate);
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new Account to the existing Accounts list
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Account object to add</returns>
        public Guid AddAccount(IAccount account)
        {
            try
            {
                //get all Accounts
                List<IAccount> allAccounts = accountsDataAccessLayer.GetAccounts();
                long maxAccountCode = 0;
                maxAccountCode = allAccounts.Count > 0 ? allAccounts.Max(item => item.AccountNumber) : Bank.Configuration.Settings.BaseAccountNo;

                account.AccountNumber = maxAccountCode + 1;
                
                //invoke DAL
                return accountsDataAccessLayer.AddAccount(account);
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="account">Account object that contains Account details to update</param>
        /// <returns>Returns true, that indicates the Account is updated successfully</returns>
        public bool UpdateAccount(IAccount account)
        {
            try
            {
                //invoke DAL
                return accountsDataAccessLayer.UpdateAccount(account);
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes an existing Account
        /// </summary>
        /// <param name="accountID">AccountID to delete</param>
        /// <returns>Returns true, that indicates the Account is deleted successfully</returns>
        public bool DeleteAccount(Guid accountID)
        {
            try
            {
                //invoke DAL
                return accountsDataAccessLayer.DeleteAccount(accountID);
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes all Accounts belong to the specific Customers
        /// </summary>
        /// <param name="customerCode">CustomerCode to delete</param>
        /// <returns>Returns true, that indicates all Accounts belong to specific Customer are deleted successfully</returns>
        public bool DeleteAccountOfCustomers(long customerCode)
        {
            try
            {
                //invoke DAL
                return accountsDataAccessLayer.DeleteAccountOfCustomers(customerCode);
            }
            catch (AccountException)
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
