using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Models;
using NLog;
using StoreApp.Library.Interfaces;
using lib = StoreApp.Library.Models;

namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Repository for orders in the database with the required functionality
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly NewDataBaseContext _dbContext;


        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Creates order repo for data manipulation of table
        /// </summary>
        /// 


        public OrderRepository(NewDataBaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<lib.Orders> GetAll()
        {
            IEnumerable<Models.Orders> items = _dbContext.OrderHistory;
                

            return items.Select(Mapper.DaoOrderMap);
        }

        public lib.Orders GetById(int id)
        {
            return Mapper.DaoOrderMap(_dbContext.OrderHistory.Find(id));
        }

        public void Add(lib.Orders order)
        {
            _logger.Info("Adding Order");

            Models.Orders entity = Mapper.LibOrdersMap(order);
            _dbContext.Add(entity);
        }

        public IEnumerable<lib.Orders> GetOrderHistoryofCustomer(lib.Customer customer)
        {
            
          
                var entities = _dbContext.OrderHistory
                    .Include(o => o.Location)
                    .Include(o => o.Customer)
                    .Where(o => o.CustomerId == customer.customerID);
                var orders = entities.Select(e => new lib.Orders
                (
                    e.OrderId,
                    e.Date,
                    new lib.Location
                    {
                        Id = e.LocationId,
                        Name = e.Location.LocationName,
                    },
                    new lib.Customer(e.Customer.FirstName, e.Customer.LastName)
                    {
                        customerID = e.Customer.CustomerId,
                    },
                    e.TotalCost
                ));
                foreach (var order in orders.ToList())
                {
                    order.OrderLine = GetAllProducts(order.OrderId);
                }
                return orders;
            }


        public Dictionary<lib.Product, int> GetAllProducts(int id)
        {
            var entity = _dbContext.OrderHistory
                    .Include(o => o.OrderLines)
                    .First(o => o.OrderId == id);
            Dictionary<lib.Product, int> orderLines = new Dictionary<lib.Product, int>();
            foreach (var item in entity.OrderLines)
            {
                var product = _dbContext.Product.Find(item.ProductId);
                lib.Product p = new lib.Product
                {
                    ProductId = product.ProductId,
                    Name = product.ProductName,
                    Price = product.Price
                };
                orderLines.Add(p, item.Amount);
            }
            return orderLines;

        }




        public void Save()
        {
            _dbContext.SaveChanges();
        }

        IEnumerable<lib.Orders> IOrderRepository.GetOrderHistoryofLocation(lib.Location location)
        {
            var entities = _dbContext.OrderHistory
                    .Include(o => o.Location)
                    .Include(o => o.Customer)
                    .Where(o => o.LocationId == location.Id);
            var orders = entities.Select(e => new lib.Orders
            (
                e.OrderId,
                e.Date,
                new lib.Location
                {
                    Id = e.LocationId,
                    Name = e.Location.LocationName,
                },
                new lib.Customer(e.Customer.FirstName, e.Customer.LastName)
                {
                    customerID = e.Customer.CustomerId,
                
                },
                e.TotalCost
            ));
            foreach (var order in orders.ToList())
            {
                order.OrderLine = GetAllProducts(order.OrderId);
            }
            return orders;
        }
    }
}
