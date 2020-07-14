using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<Product, int> Inventory { get; set; } = new Dictionary<Product, int>();
        public List<Orders> OrderHistory { get; set; } = new List<Orders>();


    }
}
