using System.Security.Claims;
namespace GameZone.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        private readonly IOrderService _orderService;

        public CartController(IShoppingCartService shoppingCartService, IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _shoppingCartService.GetCartItemsAsync(userId);
            var cartTotal = await _shoppingCartService.GetCartTotalAsync(userId);

            var viewModel = new CartViewModel
            {
                CartItems = cartItems,
                CartTotal = cartTotal
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int gameId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            await _shoppingCartService.AddToCartAsync(userId, gameId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _shoppingCartService.RemoveFromCartAsync(userId, gameId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _shoppingCartService.ClearCartAsync(userId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderId = await _orderService.CreateOrderAsync(userId, model.ShippingAddress);

            // Clear the cart after placing the order
            await _shoppingCartService.ClearCartAsync(userId);

            return RedirectToAction("OrderConfirmation", new { orderId });
        }


        public IActionResult Checkout()
        {
            return View();
        }

        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Order Confirmation";
            return View(order);
        }


    }
}
