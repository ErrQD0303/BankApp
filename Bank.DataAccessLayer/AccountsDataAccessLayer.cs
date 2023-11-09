using Bank.DataAccessLayer.DALContracts;
using Bank.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Bank.Exceptions;
using Bank.Entities.Contracts;

namespace Bank.DataAccessLayer
{
    public class AccountsDataAccessLayer : IAccountsDataAccessLayer
    {
        #region Fields
        private static List<IAccount> _accounts;

        #endregion

        #region Properties
        private static List<IAccount> Accounts { get => _accounts; set => _accounts = value; }
        #endregion

        #region Constructors
        static AccountsDataAccessLayer()
        {
            Accounts = new List<IAccount>();
            LoadAccountsFromFile();
        }
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
                //create a new Accounts list
                List<IAccount> accountsList = new List<IAccount>();

                //copy all Accounts from the source collection into the new Accounts list
                Accounts.ForEach(item => accountsList.Add(item));
                return accountsList;
            }
            catch (AccountException)
            {
                throw;
            }
            catch(Exception)
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
                //create a new Accounts list
                List<IAccount> accountsList = new List<IAccount>();

                //filter the collection
                List<IAccount> filteredAccounts = Accounts.FindAll(predicate);

                //copy all Accounts from the source collection into the new Accounts list
                filteredAccounts.ForEach(item => accountsList.Add(item.Clone() as IAccount));
                
                return accountsList;
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
                //generate new Guid
                account.AccountID = Guid.NewGuid();

                //add Account
                Accounts.Add(account);

                //write to XML file
                WriteFile();

                return account.AccountID;
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
                //find existing Account by AccountID
                IAccount existingAccount = Accounts.Find(item => item.AccountID == account.AccountID);
            
                //update all details of Account
                if(existingAccount != null)
                {
                    existingAccount.CustomerCode = account.CustomerCode;
                    existingAccount.Balance = account.Balance;

                    //write to XML file
                    WriteFile();

                    return true; //indicates the Account is updated
                }
                else
                {
                    return false; //indicaets no object is updated
                }
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
                //delete Account by AccountID
                if (Accounts.RemoveAll(item => item.AccountID == accountID) > 0)
                {
                    //write to XML file
                    WriteFile();

                    return true; //indicates one or more Accounts are deleted
                }
                else
                {
                    return false;
                }
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
                //delete Account by customerCode
                if (Accounts.RemoveAll(item => item.CustomerCode == customerCode) > 0)
                {
                    //write to XML file
                    WriteFile();

                    return true; //indicates all Accounts of Customer has been deleted
                }
                else
                {
                    return false;
                }
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
        /// Write Data to XML file
        /// </summary>
        private static void WriteFile()
        {
            List<Account> accounts = new List<Account>();
            Accounts.ForEach(item => accounts.Add(item as Account));
            XmlSerializer serializer = new XmlSerializer(typeof(List<Account>));
            using (StreamWriter sw = new StreamWriter(@"C:\Users\ADMIN\source\repos\BankApp\Bank.DataAccessLayer\DataFiles\AccountsList.xml"))
            {
                serializer.Serialize(sw, accounts);
            }
        }

        private static void LoadAccountsFromFile()
        {
            List<Account> accounts;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Account>));
            using (StreamReader sr = new StreamReader(@"C:\Users\ADMIN\source\repos\BankApp\Bank.DataAccessLayer\DataFiles\AccountsList.xml"))
            {
                accounts = serializer.Deserialize(sr) as List<Account>;
            }

            accounts.ForEach(item => Accounts.Add(item));
        }
        #endregion
    }
}
