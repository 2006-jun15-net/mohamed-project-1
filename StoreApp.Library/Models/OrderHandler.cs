using StoreApp.Library.Interfaces;

namespace StoreApp.Library.Models
{
    public class OrderHandler
    {
        private readonly ILocationRepository _location;
        private readonly IOrderRepository _order;

        public OrderHandler(ILocationRepository location, IOrderRepository order)
        {
            _location = location;
            _order = order;
        }

        public void CreateOrder(ShoppingCart cart, Customer customer)
        {
            double orderTotal = 0;
            foreach (var product in cart.Items.Keys)
            {
                cart.Location.Inventory[product] -= cart.Items[product];
                orderTotal += product.Price * cart.Items[product];
            }

            var order = new Orders(cart, customer, orderTotal);

            _order.Add(order);
            _location.Update(cart.Location);
        }
    }
}
