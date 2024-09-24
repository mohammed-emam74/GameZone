namespace GameZone.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(string userId, string shippingAddress);
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}
