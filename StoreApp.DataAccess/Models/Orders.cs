using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderLines = new HashSet<OrderLine>();
            this.Date = DateTime.Now;
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double TotalCost { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
