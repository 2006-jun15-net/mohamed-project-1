using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Inventory
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public Location Location { get; set; } = new Location();
        public Product Product { get; set; } = new Product();
    }
}
