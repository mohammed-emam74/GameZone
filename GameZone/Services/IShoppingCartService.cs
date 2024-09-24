namespace GameZone.Services
{
    public interface IShoppingCartService
    {
        Task AddToCartAsync(string userId, int gameId, int quantity);
        Task RemoveFromCartAsync(string userId, int gameId);
        Task ClearCartAsync(string userId);
        Task<List<ShoppingCartItem>> GetCartItemsAsync(string userId); 
        Task<decimal> GetCartTotalAsync(string userId);
    }


}
