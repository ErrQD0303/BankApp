using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    partial class Program
    {
        static void MainMenu()
        {
            //display title
            Console.WriteLine("*********** Bank ***************");
            Console.WriteLine("::Login Page::");

            //declare variables to store username and password;
            string userName = null, password = null;

            //read userName from keyboard
            Console.Write("Username (Press ENTER to exit): ");
            userName = Console.ReadLine();

            //read password from keyboard only if username is entered
            if (userName != "")
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
            }

            //check username and password
            if (userName == "admin" && password == "admin")
            {
                //declare variable to store menu choice
                int mainMenuChoice = -1;

                do
                {
                    Console.Clear();
                    //show main menu
                    Console.WriteLine(":::Main Menu:::");
                    Console.WriteLine("1. Customers");
                    Console.WriteLine("2. Accounts");
                    Console.WriteLine("3. Funds Transfer");
                    Console.WriteLine("4. Account Statement");
                    Console.WriteLine("0. Exit");

                    //accept menu choice from keyboard
                    Console.WriteLine("Enter choice: ");
                    mainMenuChoice = int.Parse(Console.ReadLine());

                    //switch-case to check menu choice
                    switch (mainMenuChoice)
                    {
                        case 1: CustomersMenu(); break;
                        case 2: AccountsMenu(); break;
                        case 3: FundsTransferPresentation.FundsTransfer(); break;
                        case 4: AccountStatementPresentation.AccountStatement(); break;
                        case 0: return;
                    }
                }
                while (mainMenuChoice != 0);
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }

            //about to exit
            Console.WriteLine("Thank you! Visit again.");
            Console.ReadKey();
        }
        static void CustomersMenu()
        {
            //variable to store customers menu choice
            int customerMenuChoice = -1;

            //do-while loop starts
            do
            {
                Console.Clear();
                //print customers menu
                Console.WriteLine(":::Customers menu:::");
                Console.WriteLine("1.Add Customer");
                Console.WriteLine("2.Delete Customer");
                Console.WriteLine("3.Update Customer");
                Console.WriteLine("4.Search Customer");
                Console.WriteLine("5.View Customer");
                Console.WriteLine("0.Back to Main Menu");

                //accept customers menu choice
                Console.WriteLine("Enter choice:");
                customerMenuChoice = Convert.ToInt32(Console.ReadLine());

                //switch case
                switch (customerMenuChoice)
                {
                    case 1:
                        {
                            CustomersPresentation.AddCustomer();
                            break;
                        }
                    case 2:
                        {
                            CustomersPresentation.DeleteCustomer();
                            break;
                        }
                    case 3:
                        {
                            CustomersPresentation.UpdateCustomer();
                            break;
                        }
                    case 4:
                        {
                            CustomersPresentation.SearchCustomer();
                            break;
                        }
                    case 5:
                        {
                            CustomersPresentation.ViewCustomers();
                            break;
                        }
                }
            }
            while (customerMenuChoice != 0);
        }
        static void AccountsMenu()
        {
            //variable to store accounts menu choice
            int accountsMenuChoice = -1;

            //do-while loop starts
            do
            {
                Console.Clear();
                //print accounts menu
                Console.WriteLine(":::Accounts menu:::");
                Console.WriteLine("1. Add Account");
                Console.WriteLine("2. Delete Account");
                Console.WriteLine("3. Update Account");
                Console.WriteLine("4. Search Account");
                Console.WriteLine("5. View Account");
                Console.WriteLine("0. Back to Main Menu");

                //accept accounts menu choice
                Console.WriteLine("Enter choice:");
                accountsMenuChoice = Convert.ToInt32(Console.ReadLine());

                //switch case
                switch (accountsMenuChoice)
                {
                    case 1: AccountsPresentation.AddAccount(); break;
                    case 2: AccountsPresentation.DeleteAccounts(); break;
                    case 3: AccountsPresentation.UpdateAccounts(); break;
                    case 4: AccountsPresentation.SearchAccounts(); break;
                    case 5: AccountsPresentation.ViewAccounts(); break;
                }
            } while (accountsMenuChoice != 0);
        }
    }
}
