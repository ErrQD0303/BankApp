using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.BusinessLogicLayer.BALContracts;
using Bank.DataAccessLayer.DALContracts;
using Bank.DataAccessLayer;
using Bank.Entities;
using Bank.Exceptions;
using Bank.Entities.Contracts;

namespace BusinessLogicLayer
{
    /// <summary>
    /// Represents customer business logic
    /// </summary>
    public class CustomersBusinessLogicLayer : ICustomersBusinessLogicLayer
    {
        #region Private Fields
        private ICustomersDataAccessLayer _customersDataAccessLayer;
        #endregion

        #region Constructors
        public CustomersBusinessLogicLayer()
        {
            CustomersDataAccessLayer = new CustomersDataAccessLayer();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Private property that represents reference of CustomersDataAccessLayer
        /// </summary>
        private ICustomersDataAccessLayer CustomersDataAccessLayer { get => _customersDataAccessLayer; set => _customersDataAccessLayer = value; }
        #endregion

        #region Methods
        /// <summary>
        /// Returns all existing customers
        /// </summary>
        /// <returns>List of Customers</returns>
        public List<Customer> GetCustomers()
        {
            try
            {
                //invoke DAL
                return CustomersDataAccessLayer.GetCustomers();
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
        /// Returns a set of customers that matches with specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression that contains condition to check</param>
        /// <returns>The list of matching customers</returns>
        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                //invoke DAL
                return CustomersDataAccessLayer.GetCustomersByCondition(predicate);
            }
            catch(CustomerException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new customer to the existing customers list
        /// </summary>
        /// <param name="customer">The customer object to add</param>
        /// <returns>Returns true, that indicates the customer is updated successfully</returns>
        public Guid AddCustomer(Customer customer)
        {
            try
            {
                //get all customers
                List<Customer> allCustomers = CustomersDataAccessLayer.GetCustomers();
                long maxCustCode = 0;
                maxCustCode = allCustomers.Count > 0 ? allCustomers.Max(c => c.CustomerCode) : 0;

                //generate new customer no
                if (allCustomers.Count > 0)
                {
                    customer.CustomerCode = maxCustCode + 1;
                }
                else
                {
                    customer.CustomerCode = Bank.Configuration.Settings.BaseCustomerNo + 1;
                }
                //invoke DAL
                return CustomersDataAccessLayer.AddCustomer(customer);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <param name="customer">Customer object that contains customer details to update</param>
        /// <returns>Returns true, that indicates the customer is updated successfully</returns>
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                //invoke DAL
                return CustomersDataAccessLayer.UpdateCustomer(customer);
            }
            catch(CustomerException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes an existing customer
        /// </summary>
        /// <param name="customerID">CustomerID to delete</param>
        /// <returns>Returns true, that indicates the customer is deleted successfully</returns>
        public bool DeleteCustomer(Guid customerID)
        {
            try
            {
                //invoke DAL
                return CustomersDataAccessLayer.DeleteCustomer(customerID);
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
        #endregion
    }
}
