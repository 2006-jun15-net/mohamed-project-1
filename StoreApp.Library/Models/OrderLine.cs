namespace StoreApp.Library.Models
{
    public class OrderLine
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public virtual Location Location { get; set; } = new Location();
        public virtual Product Product { get; set; } = new Product();
        public virtual Orders OrderNavigation { get; set; } = new Orders();
    }
}