namespace PizzaRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Cart;
    using static PizzaRestaurant.Common.NotificationMessagesConstants;

    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService _cartService)
        {
            this.cartService = _cartService;
        }

        [HttpGet]
        public IActionResult OrderConfirmation()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> FinalizeOrder()
        {
            string userId = GetUserId();
            List<CartItemViewModel> cartItems = await cartService.GetAllCartItemsAsync(userId);
            decimal finalPrize = await cartService.GetFinalPrizeAsync(userId);

            ViewCartViewModel model = new ViewCartViewModel()
            {
                Items = cartItems,
                FinalPrize = finalPrize
            };

            return View(model);
        }
    

        [HttpPost]
        public async Task<IActionResult> AddToCart(int pizzaId, decimal updatedTotalPrice)
        {
            try
            {
                string userId = GetUserId();
                await cartService.AddPizzaToCartAsync(pizzaId, updatedTotalPrice, userId);
                TempData[SuccessMessage] = "Pizza was successfuly added to the cart";

                return RedirectToAction("OrderConfirmation", "Cart");

            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred! Please try again later or contact administrator!");

                return RedirectToAction("All", "Pizza");
            }
        }
    }
}
