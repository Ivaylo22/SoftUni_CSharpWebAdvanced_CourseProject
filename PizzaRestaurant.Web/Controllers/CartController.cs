using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace PizzaRestaurant.Web.Controllers
{
    public class CartController : BaseController
    {
        [HttpPost]
        public IActionResult AddToCart(int pizzaId, decimal updatedTotalPrice)
        {

            

            // Redirect to a page, return a JSON response, or perform other necessary actions.
            return RedirectToAction("CartContents"); // Redirect to the cart contents page.
        }
    }
}
