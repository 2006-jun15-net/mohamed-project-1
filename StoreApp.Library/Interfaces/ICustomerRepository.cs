using StoreApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        void Add(StoreApp.Library.Models.Customer customer);
        void Delete(int customerID);
        void Delete(Customer customer);
        void Update(Customer customer);
        void Save();
        Customer SearchCustomerByName(string search);


    }
}
