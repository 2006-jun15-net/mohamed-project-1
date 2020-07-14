using System;
using System.Collections.Generic;
using StoreApp.Library.Interfaces;
using System.Linq;
using lib = StoreApp.Library.Models;
using StoreApp.DataAccess.Models;
using NLog;

namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Repository for customers in the database with the required functionality
    /// </summary>
    public class CustomerRepository : ICustomerRepository 
    {
        private readonly NewDataBaseContext _dbContext;


        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Creates customer repo for data manipulation of table
        /// </summary>
        /// 


        public CustomerRepository(NewDataBaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public void Add(lib.Customer customer)
        {

            _logger.Info("Adding Customer");

            var entity = Mapper.MapLibCustomer(customer);
            _dbContext.Customer.Add(entity);
        }

        public void Delete(int customerID)
        {
            _logger.Info("Deleting Customer with ID {customerID}", customerID);
            Customer entity = _dbContext.Customer.Find(customerID);
            _dbContext.Remove(entity);
        }

        public void Delete(lib.Customer customer)
        {
            var entity = _dbContext.Customer.First(c => c.FirstName.Equals(customer.FirstName) && c.LastName.Equals(customer.LastName));
            _dbContext.Customer.Remove(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Display all customers from the Customer table
        /// </summary>
        //public void DisplayCustomers()
        //{

        //    Console.WriteLine("\n------------------------------------------------------------------------------------------\n");
        //    Console.WriteLine("Customers in Database:\n ");
        //    foreach(var item in GetAll().ToList())
        //    {
        //        Console.WriteLine($"Name: {item.FirstName} {item.LastName} CustomerID: {item.customerID}\n");
        //    }
        //}

        public lib.Customer GetById(int id)
        {
            return Mapper.CustomerMapper(_dbContext.Customer.Find(id));

        }

        public IEnumerable<lib.Customer> GetAll()
        {
            var items = _dbContext.Customer.ToList();

            
            return items.Select(Mapper.CustomerMapper); //(IEnumerable<lib.Customer>)_dbContext.Set<Customer>();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void Update(lib.Customer customer)
        {
            var entity = _dbContext.Customer.First(c => c.CustomerId == customer.customerID);
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            _dbContext.Customer.Update(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Takes in customer name and returns first instance of that customer
        /// </summary>
        /// 


        public lib.Customer SearchCustomerByName(string search)
        {
            string x = search.ToLower();
            //lower cases the query and matches the query to lowercase first names
            if (_dbContext.Customer.Any(cust => cust.FirstName.ToLower().Equals(x)))
            {
                lib.Customer customer = Mapper.CustomerMapper(_dbContext.Customer.First(cust => cust.FirstName.ToLower().Equals(x)));
                return customer;
            }
            //if cant find a customer, than display error message...need to implement try catches;
            else
            {
                Console.WriteLine($"\nNo customers exist with this name.\n");
                return null;
            }

        }


    }
}
