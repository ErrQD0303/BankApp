using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Entities;
using Bank.Exceptions;
using Bank.DataAccessLayer.DALContracts;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Bank.DataAccessLayer
{
    public class CustomersDataAccessLayer : ICustomersDataAccessLayer
    {
        #region Fields
        private static List<Customer> _customers;
        #endregion

        #region Properties
        private static List<Customer> Customers { get => _customers; set => _customers = value; }
        #endregion

        #region Constructors
        static CustomersDataAccessLayer()
        {
            LoadCustomersFromFile();
            if (Customers == null)
            {
                Customers = new List<Customer>();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns all existing customers
        /// </summary>
        /// <returns>Custoemrs list</returns>
        public List<Customer> GetCustomers()
        {
            try
            {
                //create a new customers list
                List<Customer> customersList = new List<Customer>();

                //copy all customers from the source collection into the newCustomers list
                Customers.ForEach(item => customersList.Add((Customer)item.Clone()));
                return customersList;
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Returns list of customers that are matching with specified criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of matching customers</returns>
        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                //create a new customers list
                List<Customer> customersList = new List<Customer>();

                //filter the collection
                List<Customer> filteredCustomers = Customers.FindAll(predicate);

                //copy all customers from the source collection into the new Customers list
                filteredCustomers.ForEach(item => customersList.Add(item.Clone() as Customer));

                return customersList;
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new customer to the existing customers list
        /// </summary>
        /// <param name="customer">The customer object to add</param>
        /// <returns>Returns Guid, that indicates the customer is added successfully</returns>
        public Guid AddCustomer(Customer customer)
        {

            try
            {
                //generate new Guild
                customer.CustomerID = Guid.NewGuid();

                //add customer
                Customers.Add(customer);

                //write to json file
                WriteFile();

                return customer.CustomerID;
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an existing customer's details
        /// </summary>
        /// <param name="customer">Customer object with updated details</param>
        /// <returns>Determines whether the customer is updated or not</returns>
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                //find existing customer by CustomerID
                Customer existingCustomer = Customers.Find(item => item.CustomerID == customer.CustomerID);

                //update all details of customer
                if (existingCustomer != null)
                {
                    //existingCustomer.CustomerCode = customer.CustomerCode;
                    if (customer.CustomerName != String.Empty)
                    {
                        existingCustomer.CustomerName = customer.CustomerName;
                    }
                    if (customer.Address != String.Empty)
                    {
                        existingCustomer.Address = customer.Address;
                    }
                    if (customer.Landmark != String.Empty)
                    {
                        existingCustomer.Landmark = customer.Landmark;
                    }
                    if (customer.City != String.Empty)
                    {
                        existingCustomer.City = customer.City;
                    }
                    if (customer.Country != String.Empty)
                    {
                        existingCustomer.Country = customer.Country;
                    }
                    if (customer.Mobile != String.Empty)
                    {
                        existingCustomer.Mobile = customer.Mobile;
                    }

                    //write to json file
                    WriteFile();

                    return true; //indicates the customer is updated
                }
                else
                {
                    return false; //indicates no object is updated
                }
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes an existing customer based on CustomerID
        /// </summary>
        /// <param name="customer">CustomerID to delete</param>
        /// <returns>Indicates whether the customer is deleted or not</returns>
        public bool DeleteCustomer(Guid customerID)
        {
            try
            {
                //delete customer by CustomerID
                if (Customers.RemoveAll(item => item.CustomerID == customerID) > 0)
                {
                    //write to json file
                    WriteFile();

                    return true; //indicates one or more customers are deleted
                }
                else
                {
                    return false;
                }
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Write Data to Json file
        /// </summary>
        private static void WriteFile()
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\ADMIN\source\repos\BankApp\Bank.DataAccessLayer\DataFiles\CustomersList.json"))
            {
                string customersList = JsonConvert.SerializeObject(Customers);
                sw.Write(customersList);
            }
        }

        /// <summary>
        /// Load Customers List from file
        /// </summary>
        private static void LoadCustomersFromFile()
        {
            using (StreamReader sr = new StreamReader(@"C:\Users\ADMIN\source\repos\BankApp\Bank.DataAccessLayer\DataFiles\CustomersList.json"))
            {
                Customers = JsonConvert.DeserializeObject<List<Customer>>(sr.ReadToEnd());
            }
        }

        //private static Regex _regex =
        //new Regex(@"(\\u(?<Value>[a-zA-Z0-9]{4}))+", RegexOptions.Compiled);
        //private static string ConvertUnicodeEscapeSequencetoUTF8Characters(string sourceContent)
        //{
        //    //Check https://stackoverflow.com/questions/9738282/replace-unicode-escape-sequences-in-a-string
        //    return _regex.Replace(
        //        sourceContent, m =>
        //        {
        //            var urlEncoded = m.Groups[0].Value.Replace(@"\u00", "%");
        //            var urlDecoded = System.Web.HttpUtility.UrlDecode(urlEncoded);
        //            return urlDecoded;
        //        }
        //    );
        //}
        #endregion
    }
}
