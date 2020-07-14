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
        public string Store { get; set; }
        public List<Order> OrderHistory { get; set; }

        public string Name
        {
            get => FirstName + " " + LastName;
        }
        public Customer(string firstname, string lastname)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        //Add order, update order history list
    }
}
