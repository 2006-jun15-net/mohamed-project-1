using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLog;
using StoreApp.DataAccess.Models;
using StoreApp.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using lib = StoreApp.Library.Models;
using System.Text;

namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Repository for Products table in the database with the required functionality
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly NewDataBaseContext _dbContext;


        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Creates customer repo for data manipulation of table
        /// </summary>
        /// 


        public ProductRepository(NewDataBaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Displays all products in database
        /// </summary>
        public void DisplayProducts()
        {   //loops through products in data base with name and price
            Console.WriteLine("All Products:\n");
            foreach (var item in GetAll().ToList())
            {
                Console.WriteLine($"Product Name: {item.Name} ProductID: {item.ProductId}\n");
            }
        }

        public IEnumerable<lib.Product> GetAll()
        {
            IEnumerable<Product> items = _dbContext.Product;

            return items.Select(Mapper.DaoMapProduct);
        }

        public lib.Product GetById(int id)
        {
            return Mapper.DaoMapProduct(_dbContext.Product.Find(id));
        }

        public void Add(lib.Product product)
        {
            _logger.Info("Adding Product");

            Product entity = Mapper.MapLibProduct(product);
            _dbContext.Add(entity);
        }

        public void Delete(int id)
        {
            _logger.Info("Deleting Product with ID {id}", id);
            Product entity = _dbContext.Product.Find(id);
            _dbContext.Remove(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
