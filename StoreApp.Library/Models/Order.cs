using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace StoreApp.Library.Models
{
    /// <summary>
    /// An order placed by a customer containing details such as the location, time of order, ordertotal, etc.
    /// </summary>
    public class Order
    {
        public Location StoreLocation { get; set; }

        public DateTime OrderTime { get; set; }
        
        public Dictionary<Product, int> purchaseItems = new Dictionary<Product, int>();
                
        public Customer Customer { get; set; }

        public int OrderId { get; set; }

        public Order(Customer customer, Location store)
        {
            this.Customer = customer;
            this.StoreLocation = store;
        }

        public void AddProduct(string product, int quantity, Location store)
        {
            if(quantity >= 50)
            {
                Console.WriteLine("Please enter a lower quantity");
            }
            else
            {
                    purchaseItems.Add(Location.GetProduct(product, quantity), quantity);
                
            }
        }
        public void CompleteOrder()
        {
            OrderTime = DateTime.Now;
        }

        public void ReviewOrder()
        {
            double orderTotal=0;

            foreach (KeyValuePair<Product, int> item in purchaseItems)
            {
                Console.WriteLine("Item: {0}, Quantity: {1}", item.Key.Name, item.Value);
                orderTotal += item.Key.Price * item.Value;
            }

            Console.WriteLine("Your order total is: {0}", orderTotal);
        }


    }
}
