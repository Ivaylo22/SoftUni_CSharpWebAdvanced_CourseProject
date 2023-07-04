namespace PizzaRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Menu;
    using System.Collections.Specialized;
    using static PizzaRestaurant.Common.NotificationMessagesConstants;
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

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<MenuViewModel> model = await menuService.GetAllMenusAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMenuViewModel model)
        {

            if(!ModelState.IsValid)
            {
                //this.TempData[ErrorMessage] = "You must become an agent in order to add new houses!";
                return View(model);
            }

            await menuService.AddMenuAsync(model);

            return RedirectToAction(nameof(All));
        }
    }
}
