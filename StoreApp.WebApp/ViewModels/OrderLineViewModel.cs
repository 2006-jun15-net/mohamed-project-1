using System;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.WebApp.ViewModels
{
    public class OrderLineViewModel
    {
        [Display(Name = "OrderId")]
        public int OrderId { get; set; }

        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Amount")]
        public int Amount { get; set; }
    }
}
