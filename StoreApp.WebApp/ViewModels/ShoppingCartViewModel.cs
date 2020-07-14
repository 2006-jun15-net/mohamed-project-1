using System;
using StoreApp.Library.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace StoreApp.WebApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        [Display(Name = "Items")]
        public Dictionary<Product, int> Items { get; set; }

        [Display(Name = "Location Id")]
        public int LocationId { get; set; }

        [Display(Name = "Total")]
        private double _Total;


        public double Total
        {
            get
            {
                foreach (var item in Items.Keys)
                {
                    _Total += item.Price * Items[item];
                }
                return _Total;
            }
        }
    }
}
