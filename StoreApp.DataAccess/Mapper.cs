using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreApp.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.DataAccess
{
    public class Mapper
    {
        /// <summary>
        /// Maps an Entity Framework StoreApp DAO to a Library business model,
        /// </summary>
        /// <param name="customer">The customer DAO.</param>
        /// <returns>The customer business model.</returns>
        /// 
        public static Library.Models.Customer CustomerMapper(Customer customer)
        {
            return new Library.Models.Customer(customer.FirstName,customer.LastName)
            {
                //FirstName = customer.FirstName,
                //LastName = customer.LastName,
                customerID = customer.CustomerId,
                //OrderHistory= customer.Orders.Select(DaoOrderMap).ToList()
            };
        }

        public static Models.Customer MapLibCustomer(Library.Models.Customer customer)
        {
            return new Models.Customer()
            {
                //CustomerId = customer.customerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                //Orders = customer.OrderHistory.Select(LibOrdersMap).ToList()
            };
        }

        public static Library.Models.Orders DaoOrderMap(Orders order)
        {
            return new Library.Models.Orders()
            {
                StoreLocation = DaoLocationMap(order.Location),
                Customer = CustomerMapper(order.Customer),
                OrderId = order.OrderId,
                OrderLines = order.OrderLines.Select(DaoOrderlinesMap).ToList(),
                OrderTime = order.Date,
                totalCost = order.TotalCost
            };
        }

        public static Library.Models.OrderLine DaoOrderlinesMap(OrderLine orderLine)
        {
            return new Library.Models.OrderLine()
            {
                OrderId = orderLine.OrderId,
                ProductId = orderLine.ProductId,
                Amount = orderLine.Amount,
                Location = DaoLocationMap(orderLine.Location),
                Product = DaoMapProduct(orderLine.Product),
                OrderNavigation = DaoOrderMap(orderLine.OrderNavigation)
            };
        }


        public static Orders LibOrdersMap(Library.Models.Orders orders)
        {
            return new Orders()
            {
                OrderId = orders.OrderId,
                CustomerId = orders.Customer.customerID,
                LocationId = orders.StoreLocation.Id,
                Date = orders.OrderTime,
                TotalCost = orders.totalCost,
                Customer = MapLibCustomer(orders.Customer),
                Location = MapLibLocation(orders.StoreLocation),
                OrderLines = orders.OrderLines.Select(LibOrderLinesMap).ToList()
            };
        }

        public static OrderLine LibOrderLinesMap(Library.Models.OrderLine orderLine)
        {
            return new OrderLine()
            {
                OrderId = orderLine.OrderId,
                ProductId = orderLine.ProductId,
                Amount = orderLine.Amount,
                Location = MapLibLocation(orderLine.Location),
                Product = MapLibProduct(orderLine.Product)
            };
        }

        public static Product MapLibProduct(Library.Models.Product product)
        {
            return new Product()
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Price = product.Price,
                Inventory = product.Inventory.Select(LibInventoryMap).ToList(),
                OrderLine = product.OrderLine.Select(LibOrderLinesMap).ToList()
            };
        }

        public static Inventory LibInventoryMap(Library.Models.Inventory inventory)
        {
            return new Inventory()
            {
                LocationId = inventory.LocationId,
                ProductId = inventory.ProductId,
                Amount = inventory.Amount,
                Location = MapLibLocation(inventory.Location),
                Product = MapLibProduct(inventory.Product)
            };
        }

        public static Location MapLibLocation(Library.Models.Location storeLocation)
        {
            return new Location()
            {
                LocationId = storeLocation.Id,
                LocationName = storeLocation.Name,
                OrderHistory = storeLocation.OrderHistory.Select(LibOrdersMap).ToList()
            };
        }

        public static Library.Models.Product DaoMapProduct(Product product)
        {
            return new Library.Models.Product()
            {
                Price = product.Price,
                Name = product.ProductName,
                ProductId = product.ProductId
    };
        }

        public static Library.Models.Location DaoLocationMap(Location location)
        {
            return new Library.Models.Location()
            {
                Id = location.LocationId,
                Name = location.LocationName,
                OrderHistory = location.OrderHistory.Select(DaoOrderMap).ToList(),
            };
        }

        public static Library.Models.Inventory DaoInventoryMap(Inventory inventory)
        {
            return new Library.Models.Inventory()
            {
                LocationId = inventory.LocationId,
                ProductId = inventory.ProductId,
                Amount = inventory.Amount,
                Location = DaoLocationMap(inventory.Location),
                Product = DaoMapProduct(inventory.Product)
            };
        }
    }
}
