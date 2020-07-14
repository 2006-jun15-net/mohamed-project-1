using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreApp.Library.Models;

namespace StoreApp.WebApp.ViewModels
{
    public class OrdersViewModel
    {
        [Display(Name ="Location Name")]
        public string Location { get; set; }

        [Display(Name = "Order Time")]
        public DateTime OrderDate { get; set; }

        public Dictionary<Product, int> OrderLine { get; set; }

        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }

        [Display(Name = "Order Id")]
        public int OrderId { get; set; }
        
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }
    }
}
