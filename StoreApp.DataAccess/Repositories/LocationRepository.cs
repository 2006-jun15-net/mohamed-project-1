using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using StoreApp.DataAccess.Models;
using StoreApp.Library.Interfaces;
using lib = StoreApp.Library.Models;

namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Repository for Stores(Location) table in the database with the required functionality
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        private readonly NewDataBaseContext _dbContext;


        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Creates customer repo for data manipulation of table
        /// </summary>
        /// 


        public LocationRepository(NewDataBaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<lib.Location> GetAll()
        {
            var entities = _dbContext.Location.ToList();

            return entities.Select(e => new lib.Location
            {
                Id = e.LocationId,
                Name = e.LocationName
            });
        }
        public Dictionary<lib.Product, int> GetAllProducts(int id)
        {
            var entity = _dbContext.Location
                    .First(s => s.LocationId == id);
            Dictionary<lib.Product, int> inventory = new Dictionary<lib.Product, int>();
            foreach (var item in entity.Inventory)
            {
                var product = _dbContext.Product.Find(item.ProductId);
                Product p = new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price
                };
                inventory.Add(Mapper.DaoMapProduct(p), item.Amount);
            }
            return inventory;
        }

        public lib.Location GetById(int id)
        {
            return Mapper.DaoLocationMap(_dbContext.Location.Find(id));
        }

        public void Add(lib.Location location)
        {
            _logger.Info("Adding Location");

            Location entity = Mapper.MapLibLocation(location);
            _dbContext.Add(entity);
        }

        public void Delete(int id)
        {
            _logger.Info("Deleting Location with ID {id}", id);
            Location entity = _dbContext.Location.Find(id);
            _dbContext.Remove(entity);
        }
        public void Update(lib.Location location)
        {
            var entity = _dbContext.Location
                .First(s => s.LocationId == location.Id);
            foreach (var item in location.Inventory.Keys)
            {
                entity.Inventory.First(i => i.ProductId == item.ProductId).Amount = location.Inventory[item];
            }
            _dbContext.SaveChanges();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Display all locations available in the locations table using get all to list
        /// </summary>
        public void DisplayLocations()
        {
            Console.WriteLine("List of Stores:\n");
            foreach (var item in GetAll().ToList())
            {
                Console.WriteLine($"Location: {item.Name} ID: {item.Id}\n");
            }
        }

        
    }
}
