using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Product
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }

        public List<Inventory> Inventory { get; set; } = new List<Inventory>();

        public List<OrderLine> OrderLine { get; set; } = new List<OrderLine>();


    }
}
