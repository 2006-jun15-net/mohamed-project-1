using StoreApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Orders> GetAll();
        Orders GetById(int id);
        void Add(Orders order);
        Dictionary<Product, int> GetAllProducts(int id);
        IEnumerable<Orders> GetOrderHistoryofCustomer(Customer customer);
        IEnumerable<Orders> GetOrderHistoryofLocation(Location location);

    }
}
