using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreApp.Library.Models;

namespace StoreApp.WebApp.ViewModels
{
    public class LocationViewModel
    {

        public int LocationId { get; set; }

        public string Name { get; set; }

        public List<OrdersViewModel> OrderHistory { get; set; }
    }
}
