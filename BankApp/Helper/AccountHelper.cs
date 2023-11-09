using Bank.Entities.Contracts;
using BankApp.Contracts;
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp.Helper
{
    public static class AccountHelper
    {
        public static CustomersBusinessLogicLayer customersBusinessLogicLayer { get; set; } = new CustomersBusinessLogicLayer();
        public static AccountsBusinessLogicLayer accountsBusinessLogicLayer { get; set; } = new AccountsBusinessLogicLayer();
        public static  TransactionsBusinessLogicLayer transactionsBusinessLogicLayer { get; set; } = new  TransactionsBusinessLogicLayer();
        public static Type GetAccountType()
        {
            //create an object of Account using reflection
            Assembly assembly = Assembly.LoadFile(@"C:\Users\ADMIN\source\repos\BankApp\BankApp\bin\Debug\Bank.Entities.dll");

            //using Reflection to get the type
            return assembly.GetTypes().Where(t => typeof(IAccount).IsAssignableFrom(t) && Regex.IsMatch(t.Name, "^Account$")).FirstOrDefault();
        }
        public static Type GetTransactionType()
        {
            //create an object of   Transaction using reflection
            Assembly assembly = Assembly.LoadFile(@"C:\Users\ADMIN\source\repos\BankApp\BankApp\bin\Debug\Bank.Entities.dll");

            //using Reflection to get the type
            return assembly.GetTypes().Where(t => typeof(ITransaction).IsAssignableFrom(t) && Regex.IsMatch(t.Name, "^Transaction$")).FirstOrDefault();
        }
    }
}
