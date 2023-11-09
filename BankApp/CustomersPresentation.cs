using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Entities;
using Bank.Exceptions;
using Bank.BusinessLogicLayer;
using Bank.BusinessLogicLayer.BALContracts;
using BusinessLogicLayer;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Bank.Entities.Contracts;
using BankApp.Helper;

namespace BankApp
{
    internal static class CustomersPresentation
    {
        internal static void AddCustomer()
        {
            try
            {
                Console.Clear();
                //create an object of Customer
                Customer customer = new Customer();

                //read all details from the user
                Console.WriteLine("********ADD CUSTOMER***********");
                Console.Write("Customer Name: ");
                customer.CustomerName = Console.ReadLine();
                Console.Write("Address: ");
                customer.Address = Console.ReadLine();
                Console.Write("Land mark: ");
                customer.Landmark = Console.ReadLine();
                Console.Write("City: ");
                customer.City = Console.ReadLine();
                Console.Write("Country: ");
                customer.Country = Console.ReadLine();
                Console.Write("Mobile: ");
                customer.Mobile = Console.ReadLine();

                //Create BL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                Guid newGuid = customersBusinessLogicLayer.AddCustomer(customer);
                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(item => item.CustomerID == newGuid);
                if (matchingCustomers.Count >= 1)
                {
                    Console.WriteLine("New Customer Code:" + matchingCustomers[0].CustomerCode);
                    Console.WriteLine("Customer Added.\n");
                }
                else
                {
                    Console.WriteLine("Customer not Added.\n");
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

        internal static void ViewCustomers()
        {
            try
            {
                Console.Clear();
                //Create BL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();
                Console.WriteLine("\n**********ALL CUSTOMERS************");

                printAllCustomers(allCustomers);
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

        internal static void DeleteCustomer()
        {
            try
            {
                Console.Clear();
                //Create BL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                Console.WriteLine("**********DELETE CUSTOMER************");

                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();
                Console.WriteLine("\n**********ALL CUSTOMERS************");

                printAllCustomers(allCustomers);

                Console.Write("Enter the Customer Code for which you want to delete: ");
                long deleteCustomerCode = long.Parse(Console.ReadLine());

                Customer deleteCustomer = allCustomers.Find(cust => cust.CustomerCode == deleteCustomerCode);

                if (deleteCustomer != null && customersBusinessLogicLayer.DeleteCustomer(deleteCustomer.CustomerID))
                {
                    Console.WriteLine("Customer Deleted!");
                }
                else
                {
                    Console.WriteLine("Wrong customer code!");
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

        internal static void UpdateCustomer()
        {
            try
            {
                Console.Clear();
                //Create BL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                Console.WriteLine("\n**********UPDATE CUSTOMERS************");

                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();
                Console.WriteLine("\n**********ALL CUSTOMERS************");

                printAllCustomers(allCustomers);

                Console.Write("Enter the Customer Code that you want to edit: ");
                long updateCustomerCode = long.Parse(Console.ReadLine());

                Customer updateCustomer = allCustomers.Find(cust => cust.CustomerCode == updateCustomerCode);

                if (updateCustomer != null)
                {
                    //create an object of Customer
                    Customer customer = new Customer { CustomerID = updateCustomer.CustomerID };

                    //read all details from the user
                    Console.WriteLine("NEW CUSTOMER DETAILS");
                    Console.Write("Customer Name: ");
                    customer.CustomerName = Console.ReadLine();
                    Console.Write("Address: ");
                    customer.Address = Console.ReadLine();
                    Console.Write("Land mark: ");
                    customer.Landmark = Console.ReadLine();
                    Console.Write("City: ");
                    customer.City = Console.ReadLine();
                    Console.Write("Country: ");
                    customer.Country = Console.ReadLine();
                    Console.Write("Mobile: ");
                    customer.Mobile = Console.ReadLine();
                    if (customersBusinessLogicLayer.UpdateCustomer(customer))
                    {
                        Console.WriteLine("Customer Updated!");
                    }
                    else
                    {
                        Console.WriteLine("Error!");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong customer code!");
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

        internal static void SearchCustomer()
        {
            try
            {
                Console.Clear();
                //Create BL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                Console.WriteLine("\n**********SEARCH CUSTOMERS************");

                Console.Write("Customer Name: ");
                string searchName = Console.ReadLine();

                List<Customer> matchesCustomers = customersBusinessLogicLayer
                    .GetCustomersByCondition(item => Regex.IsMatch(item.CustomerName, searchName));
                Console.WriteLine("\n**********CUSTOMERS************");

                printAllCustomers(matchesCustomers);
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

        internal static void printAllCustomers(IEnumerable<ICustomer> customers)
        {
            //read all customers
            foreach (var item in customers)
            {
                Console.WriteLine("Customer Code: " + item.CustomerCode);
                Console.WriteLine("Customer Name: " + item.CustomerName);
                Console.WriteLine("Address: " + item.Address);
                Console.WriteLine("Landmark: " + item.Landmark);
                Console.WriteLine("City: " + item.City);
                Console.WriteLine("Country: " + item.Country);
                Console.WriteLine("Mobile: " + item.Mobile);
                Console.WriteLine();
            }
        }
    }
}
