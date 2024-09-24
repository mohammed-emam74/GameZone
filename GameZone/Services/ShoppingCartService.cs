namespace GameZone.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(string userId, int gameId, int quantity)
        {
            var cartItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(i => i.UserId == userId && i.GameId == gameId);

            if (cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    UserId = userId,
                    GameId = gameId,
                    Quantity = quantity
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                _context.ShoppingCartItems.Update(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string userId, int gameId)
        {
            var cartItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(i => i.UserId == userId && i.GameId == gameId);

            if (cartItem != null)
            {
                _context.ShoppingCartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await _context.ShoppingCartItems
                .Where(i => i.UserId == userId).ToListAsync();

            _context.ShoppingCartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartItem>> GetCartItemsAsync(string userId) 
        {
            return await _context.ShoppingCartItems
                .Include(i => i.Game)
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<decimal> GetCartTotalAsync(string userId)
        {
            return await _context.ShoppingCartItems
                .Where(i => i.UserId == userId)
                .SumAsync(i => i.Quantity * i.Game.Price);
        }
    }

}
