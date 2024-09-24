namespace GameZone.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<ShoppingCartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }

}
