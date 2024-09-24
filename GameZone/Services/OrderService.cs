namespace GameZone.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrderAsync(string userId, string shippingAddress)
        {
            var cartItems = await _context.ShoppingCartItems
                .Where(i => i.UserId == userId)
                .Include(i => i.Game)
                .ToListAsync();

            if (cartItems.Any())
            {
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalPrice = cartItems.Sum(i => i.Game.Price * i.Quantity),
                    ShippingAddress = shippingAddress,
                    Status = "Pending" // Set a default status
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync(); // Save order first

                // You can return the OrderId here after saving
                int orderId = order.OrderId;

                foreach (var item in cartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = orderId, // Use the newly created orderId
                        GameId = item.GameId,
                        Quantity = item.Quantity,
                        Price = item.Game.Price
                    };
                    _context.OrderItems.Add(orderItem);
                }

                await _context.SaveChangesAsync(); // Save order items

                return order.OrderId; ; // Return the order ID
            }

            throw new Exception("No items in cart."); // Handle the case where the cart is empty
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }


    }
}
