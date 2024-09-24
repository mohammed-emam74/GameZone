namespace GameZone.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GameId { get; set; }
        public int Quantity { get; set; }
        public ApplicationUser User { get; set; }
        public Game Game { get; set; }
    }
}
