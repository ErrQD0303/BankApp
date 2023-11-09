using Bank.Entities;
using Bank.Entities.Contracts;
using BankApp.Contracts;
using BankApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    internal static class AccountStatementPresentation
    {
        public static void AccountStatement()
        {
            try
            {
                //Get all Accounts
                List<IAccount> allAccounts = AccountHelper.accountsBusinessLogicLayer.GetAccounts();

                Console.WriteLine("********ACCOUNT STATEMENT*************");
                Console.WriteLine("\n**********ALL ACCOUNTS*************"); 

                List<IAccount> accounts = AccountHelper.accountsBusinessLogicLayer.GetAccounts();
                List<Customer> customersList = AccountHelper.customersBusinessLogicLayer.GetCustomers();

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

                Console.Write("Enter the Account Number that you want to view: ");
                long accountNumber = long.Parse(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("********Transaction History*****************");

                Console.WriteLine("\nDebit Transactions:");
                List<ITransaction> allDebitTransactions = AccountHelper.transactionsBusinessLogicLayer
                    .GetTransactionsByCondition(item => item.transactionType == TransactionType.Debit && (item.SourceAccountNumber == accountNumber || item.DestinationAccountNumber == accountNumber));
                PrintTransactions(allDebitTransactions);

                Console.WriteLine("\nCredit Transactions:");
                List<ITransaction> allCreditTransactions = AccountHelper.transactionsBusinessLogicLayer
                    .GetTransactionsByCondition(item => item.transactionType == TransactionType.Credit && (item.SourceAccountNumber == accountNumber || item.DestinationAccountNumber == accountNumber));
                PrintTransactions(allCreditTransactions);
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
        public static void PrintTransactions(IEnumerable<ITransaction> transactions)
        {
            try
            {
                Console.WriteLine("Transaction Date, Source Account Number, Destination Account Number, Transaction Amount");
                foreach (var item in transactions)
                {
                    Console.WriteLine($"{item.TransactionDate}, {item.SourceAccountNumber}, {item.DestinationAccountNumber}, {item.Amount}");
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.WriteLogFile(ex);
            }
            finally
            {
            }
        }
    }
}
