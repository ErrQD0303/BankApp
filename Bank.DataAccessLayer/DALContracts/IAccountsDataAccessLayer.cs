using Bank.Entities;
using Bank.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccessLayer.DALContracts
{
    /// <summary>
    /// interface that represents accounts data access layer
    /// </summary>
    public interface IAccountsDataAccessLayer
    {
        /// <summary>
        /// Returns all existing Accounts of Customer
        /// </summary>
        /// <returns>List of Account</returns>
        List<IAccount> GetAccounts();

        /// <summary>
        /// Returns a set of Accounts that matches with specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression that contains condition to check</param>
        /// <returns>The list of matching Accounts</returns>
        List<IAccount> GetAccountsByCondition(Predicate<IAccount> predicate);
    
        /// <summary>
        /// Adds a new Account to the existing Accounts list
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Account object to add</returns>
        Guid AddAccount(IAccount account);

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="account">Account object that contains Account details to update</param>
        /// <returns>Returns true, that indicates the Account is updated successfully</returns>
        bool UpdateAccount(IAccount account);

        /// <summary>
        /// Deletes an existing Account
        /// </summary>
        /// <param name="accountID">AccountID to delete</param>
        /// <returns>Returns true, that indicates the Account is deleted successfully</returns>
        bool DeleteAccount(Guid accountID);

        /// <summary>
        /// Deletes all Accounts belong to the specific Customers
        /// </summary>
        /// <param name="customerCode">CustomerCode to delete</param>
        /// <returns>Returns true, that indicates all Accounts belong to specific Customer are deleted successfully</returns>
        bool DeleteAccountOfCustomers(long customerCode);
    }
}
