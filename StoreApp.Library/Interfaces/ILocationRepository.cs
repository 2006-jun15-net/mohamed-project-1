using StoreApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAll();
        Location GetById(int id);
        Dictionary<Product, int> GetAllProducts(int id);
        void Update(Location location);
        void Save();
        void DisplayLocations();
    }
}
