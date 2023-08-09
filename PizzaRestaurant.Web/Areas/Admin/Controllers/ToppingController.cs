using PizzaRestaurant.Web.Controllers;

namespace PizzaRestaurant.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Products;
    using PizzaRestaurant.Web.ViewModels.Topping;

    using static PizzaRestaurant.Common.NotificationMessagesConstants;

    public class ToppingController : BaseAdminController
    {
        private readonly IToppingService toppingService;

        public ToppingController(IToppingService _toppingService)
        {
            toppingService = _toppingService;
        }

        [HttpGet]
        public IActionResult Options()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToppingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await toppingService.AddToppingAsync(model);

            return RedirectToAction(nameof(Options));
        }

        [HttpGet]
        public async Task<IActionResult> Remove()
        {
            IEnumerable<ToppingForPizzaVIewModel> model =
                await toppingService.GetAllToppingsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveTopping(int toppingId)
        {
            try
            {
                ToppingForPizzaVIewModel model =
                    await toppingService.GetToppingByIdAsync(toppingId);
                return View(model);

            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                return RedirectToAction("Options", "Topping");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTopping(int id, ToppingForPizzaVIewModel toppingModel)
        {
            try
            {
                await toppingService.DeleteByIdAsync(id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                return View(toppingModel);
            }

            TempData[SuccessMessage] = "Topping is deleted successfully";
            return RedirectToAction("Options", "Topping");
        }
    }
}
