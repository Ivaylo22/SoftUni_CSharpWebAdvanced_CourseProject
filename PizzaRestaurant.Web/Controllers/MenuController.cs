using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Services.Data.Interfaces;

namespace PizzaRestaurant.Web.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService _menuService)
        {
            this.menuService = _menuService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async IActionResult All()
        {
            var model = await menuService.GetAllMenusAsync();

            return View(model);
        }
    }
}
