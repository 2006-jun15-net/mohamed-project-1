using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace StoreApp.Library.Models
{
    /// <summary>
    /// An order placed by a customer containing details such as the location, time of order, ordertotal, etc.
    /// </summary>
    public class Orders
    {
        public Location StoreLocation { get; set; }

        public DateTime OrderTime { get; set; }

        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public Customer Customer { get; set; } 

        public int OrderId { get; set; }

        public double totalCost { get; set; }
        public Dictionary<Product, int> OrderLine { get; set; } = new Dictionary<Product, int>();

        public Orders()
        {
        }
        public Orders(int orderId, DateTime ordertime, Location location, Customer customer, double TotalCost)
        {
            OrderId = orderId;
            OrderTime = ordertime;
            StoreLocation = location;
            Customer = customer;
            totalCost = TotalCost;
        }
        public Orders(Location location, DateTime ordertime, Dictionary<Product, int> orderLine, Customer customer, double TotalCost)
        {
            StoreLocation = location;
            OrderTime = ordertime;
            OrderLine = orderLine;
            Customer = customer;
            totalCost = TotalCost;
        }
        public Orders(ShoppingCart cart, Customer customer, double totalCost)
            : this(cart.Location, DateTime.Now, cart.Items, customer, totalCost)
        {
        }
    }
}
