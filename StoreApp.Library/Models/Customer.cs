using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Customer
    {
        public int customerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public List<Orders> OrderHistory { get; set; } = new List<Orders>();

        public Customer(string first, string second)
        {
            FirstName = first;
            LastName = second;
        }
        public string Name
        {
            get => FirstName + " " + LastName;
            set
            {
                // "value" is the value passed to the setter.
                if (value.Length == 0)
                {
                    // good practice to provide useful messages when throwing exceptions,
                    // as well as the name of the relevant parameter if applicable.
                    throw new ArgumentException("Name must not be empty.", nameof(value));
                }
                Name = value;
            }
        }
        
    }
}
