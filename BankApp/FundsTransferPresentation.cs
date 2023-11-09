using Bank.Entities;
using Bank.Entities.Contracts;
using BankApp.Contracts;
using BankApp.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    internal static class FundsTransferPresentation
    {
        public static void FundsTransfer()
        {
            try
            {
                Console.Clear();
                List<IAccount> allAccounts = AccountHelper.accountsBusinessLogicLayer.GetAccounts();
                Console.WriteLine("**********ALL ACCOUNTS************");
                IEnumerable<ICustomer> allCustomers = AccountHelper.customersBusinessLogicLayer.GetCustomers();
                List<ICustomer> customersList = allCustomers.ToList();

                //read all accounts
                foreach (var item in allAccounts)
                {
                    Console.Write("\nAccount Number: " + item.AccountNumber);
                    Console.Write("\nCustomer Code: " + item.CustomerCode);
                    string customerName = customersList.Find(c => c.CustomerCode == item.CustomerCode)?.CustomerName;
                    Console.Write("\nCustomer Name: " + customerName);
                    Console.Write("\nBalance: " + item.Balance);
                    Console.WriteLine();
                }

                Console.Write("Enter the Source Account Number: ");
                long sourceAccountNumber = long.Parse(Console.ReadLine());
                Console.Write("Enter the Destination Account Number: ");
                long destinationAccountNumber = long.Parse(Console.ReadLine());

                Console.WriteLine("Date of Transaction (YYYY-MM-DD hh:mm:ss tt): ");
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
                Console.Write("Amount: ");
                double transactAmount = double.Parse(Console.ReadLine());

                //get sourceAccount object and destinationAccount object
                IAccount sourceAccount = AccountHelper.accountsBusinessLogicLayer.GetAccountsByCondition(item => item.AccountNumber == sourceAccountNumber).FirstOrDefault();
                IAccount destinationAccount = AccountHelper.accountsBusinessLogicLayer.GetAccountsByCondition(item => item.AccountNumber == destinationAccountNumber).FirstOrDefault();

                if (sourceAccount != null && destinationAccount != null && transactAmount <= sourceAccount.Balance)
                {
                    //update Balances
                    sourceAccount.Balance -= transactAmount;
                    destinationAccount.Balance += transactAmount;

                    //update Accounts
                    AccountHelper.accountsBusinessLogicLayer.UpdateAccount(sourceAccount);
                    AccountHelper.accountsBusinessLogicLayer.UpdateAccount(destinationAccount);

                    //Transactions store
                    //Get   Transaction Type
                    Type transactionType = AccountHelper.GetTransactionType();

                    //Create Transaction instance
                    var newTransaction = Activator.CreateInstance(transactionType) as ITransaction;

                    newTransaction.SourceAccountNumber = sourceAccountNumber;
                    newTransaction.DestinationAccountNumber = destinationAccountNumber;
                    newTransaction.Amount = transactAmount;
                    newTransaction.transactionType = TransactionType.Debit;

                    Guid newTransactionID = AccountHelper.transactionsBusinessLogicLayer.AddTransaction(newTransaction);

                    //get all Transactions
                    if (AccountHelper.transactionsBusinessLogicLayer.GetTransactions().Where(item => (item.TransactionID == newTransactionID)).ToList().Count > 0)
                    {
                        Console.WriteLine("Transaction successful");
                        Console.WriteLine("Account Balance of source account number {0} is {1}.", sourceAccountNumber, sourceAccount.Balance);
                        Console.WriteLine("\nAccount Balance of destination account number {0} is {1}.", destinationAccountNumber, destinationAccount.Balance);
                    }
                }
                else if (sourceAccount == null || destinationAccount == null)
                {
                    if (sourceAccount == null)
                    {
                        Console.WriteLine("Incorrect source account!");
                    }

                    if (destinationAccount == null)
                    {
                        Console.WriteLine("Incorrect destination account!");
                    }
                }
                else
                {
                    Console.WriteLine("Amount Incorrect!");
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
