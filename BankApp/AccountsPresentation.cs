using Bank.Entities;
using Bank.Entities.Contracts;
using BankApp.Helper;
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp
{
    internal static class AccountsPresentation
    {
        internal static void AddAccount()
        {
            try
            {
                Console.Clear();

                Type accountType = AccountHelper.GetAccountType();

                CustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                AccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();
                if (accountType != null)
                {
                    Console.WriteLine("********ADD ACCOUNT***********");
                    List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();
                    Console.WriteLine("\n**********ALL CUSTOMERS************");
                    CustomersPresentation.printAllCustomers(allCustomers);

                    Console.Write("Enter the Customer Code for which you want to create a new account: ");
                    long addCustomerCode = long.Parse(Console.ReadLine());

                    ICustomer addAccountCustomer = allCustomers.Find(item => item.CustomerCode == addCustomerCode);

                    if (addAccountCustomer != null)
                    {
                        //using Activator to create instance of the Account Type
                        var newAccount = Activator.CreateInstance(accountType) as IAccount;
                        newAccount.CustomerCode = addCustomerCode;
                        Guid newAccountID = accountsBusinessLogicLayer.AddAccount(newAccount);

                        List<IAccount> matchingAccount = accountsBusinessLogicLayer.GetAccountsByCondition(item => item.AccountID == newAccountID);
                        if (matchingAccount.Count > 0)
                        {
                            Console.WriteLine("New Account Number: " + newAccountID);
                            Console.WriteLine("Account Added");
                        }
                        else
                        {
                            Console.WriteLine("Account Not Added");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Customer Code!");
                    }
                }
            }
            catch (Exception ex)
            {
                
                ExceptionHelper.WriteLogFile(ex);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        internal static void DeleteAccounts()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("**********DELETE ACCOUNT************");

                List<IAccount> allAccounts = AccountHelper.accountsBusinessLogicLayer.GetAccounts();

                Console.WriteLine("**********ALL ACCOUNTS************");
                printAllAccounts(allAccounts);

                Console.Write("Enter the Account Number for which you want to delete: ");
                long deleteAccountNumber = long.Parse(Console.ReadLine());

                IAccount deleteAccount = allAccounts.Find(item => item.AccountNumber == deleteAccountNumber);

                if (deleteAccount != null && AccountHelper.accountsBusinessLogicLayer.DeleteAccount(deleteAccount.AccountID))
                {
                    Console.WriteLine("Account Deleted!");
                }
                else
                {
                    Console.WriteLine("Wrong Account Number!");
                }
            }
            catch (Exception ex)
            {
                
                ExceptionHelper.WriteLogFile(ex);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        internal static void UpdateAccounts()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("**********UPDATE ACCOUNT************");

                List<IAccount> allAccounts = AccountHelper.accountsBusinessLogicLayer.GetAccounts();

                Console.WriteLine("**********ALL ACCOUNTS************");
                printAllAccounts(allAccounts);

                Console.Write("Enter the Account Number for which you want to update: ");
                long updateAccountNumber = long.Parse(Console.ReadLine());

                IAccount updateAccount = allAccounts.Find(item => item.AccountNumber == updateAccountNumber);

                if (updateAccount != null)
                {

                    //read all details from the user
                    Console.WriteLine("NEW ACCOUNT DETAILS");
                    Console.Write("\nAccount Number: " + updateAccount.AccountNumber);
                    Console.Write("\nCustomer Code: ");
                    updateAccount.CustomerCode = long.Parse(Console.ReadLine());
                    Console.Write("\nBalance: ");
                    updateAccount.Balance = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine("Account Updated!");
                }
                else
                {
                    Console.WriteLine("Wrong Account Number!");
                }
            }
            catch (Exception ex)
            {
                
                ExceptionHelper.WriteLogFile(ex);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        internal static void SearchAccounts()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("**********SEARCH ACCOUNT************");

                List<IAccount> allAccounts = AccountHelper.accountsBusinessLogicLayer.GetAccounts();

                Console.Write("Customer Name: ");
                string searchName = Console.ReadLine();

                List<long> matchesCustomersCodes = AccountHelper.customersBusinessLogicLayer
                    .GetCustomersByCondition(item => Regex.IsMatch(item.CustomerName, searchName)).Select(x => x.CustomerCode).Distinct().ToList();

                List<IAccount> matchedAccounts = new List<IAccount>();

                foreach (var customerCode in matchesCustomersCodes)
                {
                    AccountHelper.accountsBusinessLogicLayer
                        .GetAccountsByCondition(item => item.CustomerCode == customerCode)
                        .ToList()
                        .ForEach(item => matchedAccounts.Add(item));
                }

                Console.WriteLine("**********SEARCH ACCOUNTS************");
                printAllAccounts(matchedAccounts);
            }
            catch (Exception ex)
            {
                
                ExceptionHelper.WriteLogFile(ex);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        internal static void ViewAccounts()
        {
            try
            {
                Console.Clear();
                List<IAccount> allAccounts = AccountHelper.accountsBusinessLogicLayer.GetAccounts();
                Console.WriteLine("**********ALL ACCOUNTS************");
                printAllAccounts(allAccounts);
            }
            catch (Exception ex)
            {
                
                ExceptionHelper.WriteLogFile(ex);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        internal static void printAllAccounts(IEnumerable<IAccount> accounts)
        {
            try
            {
                //create AccountsBusinessLogicLayer instance
                CustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                IEnumerable<ICustomer> allCustomers = AccountHelper.customersBusinessLogicLayer.GetCustomers();
                List<ICustomer> customersList = allCustomers.ToList();

                //read all accounts
                foreach (var item in accounts)
                {
                    Console.Write("\nAccount Number: " + item.AccountNumber);
                    Console.Write("\nCustomer Code: " + item.CustomerCode);
                    string customerName = customersList.Find(c => c.CustomerCode == item.CustomerCode)?.CustomerName;
                    Console.Write("\nCustomer Name: " + customerName);
                    Console.Write("\nBalance: " + item.Balance);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                
                ExceptionHelper.WriteLogFile(ex);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
