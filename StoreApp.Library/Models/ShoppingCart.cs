using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class ShoppingCart
    {
        public Dictionary<Product, int> Items { get; } = new Dictionary<Product, int>();
        public Location Location { get; }

        public ShoppingCart(Location location)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        public void AddToCart(Product p, int qty)
        {
            if (qty > Location.Inventory[p])
            {
                throw new ArgumentException("Trying to order more than in stock");
            }

            Items.Add(p, qty);
        }
    }
}
