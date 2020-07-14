using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Models
{
    public partial class OrderHistory
    {
        public OrderHistory()
        {
            Order = new HashSet<Order>();
            this.Date = DateTime.Now;
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal? TotalCost { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
