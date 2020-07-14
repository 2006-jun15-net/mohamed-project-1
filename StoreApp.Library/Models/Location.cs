using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static Dictionary<Product,int> StoreInventory { get; set; }
        public static Product GetProduct(string product, int quantity)
        {
            try
            {
                foreach (KeyValuePair<Product, int> item in StoreInventory)
                {
                    if (item.Key.Name == product && item.Value>= quantity)
                    {
                        StoreInventory[item.Key] -= quantity;
                        return item.Key;
                    }
                }
                return null;
            }
            catch
            {
                Console.WriteLine("Product not available in this store.");
                return null;
            }


        }

    }
}
