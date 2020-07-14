using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public virtual OrderHistory OrderNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
