namespace PizzaRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Menu;

    using static PizzaRestaurant.Common.NotificationMessagesConstants;
    public class MenuController : BaseController
    {
        private readonly IMenuService menuService;
        private readonly IPizzaService pizzaService;


        public MenuController(IMenuService _menuService, IPizzaService _pizzaService)
        {
            this.menuService = _menuService;
            this.pizzaService = _pizzaService;

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
                return View(model);
            }

            await menuService.AddMenuAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                DeleteMenuViewModel menuModel =
                    await menuService.GetMenuForDeleteAsync(id);

                return View(menuModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                return RedirectToAction("All", "Menu");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeleteMenuViewModel menuModel)
        {
            try
            {
                await menuService.DeleteByIdAsync(id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                return View(menuModel);
            }

            return RedirectToAction("All", "Menu");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditMenuViewModel model = await menuService.GetMenuForEditAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMenuViewModel editModel)
        {
            if(!ModelState.IsValid)
            {
                return View(editModel);
            }

            try
            {
                await menuService.EditMenuByIdAndEditModelAsync(id, editModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                return View(editModel);
            }

            return RedirectToAction("All", "Menu");
        }

        [HttpGet]
        public async Task<IActionResult> RemovePizzaFromMenu(int menuId, int pizzaId)
        {
            var success = await menuService.RemovePizzaFromMenuAsync(menuId, pizzaId);

            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to remove pizza from the menu.";
            }

            return RedirectToAction("Edit", new { id = menuId });
        }

        [HttpGet]
        public async Task<IActionResult> AddPizzas(int menuId)
        {
            var model = await pizzaService.GetAllPizzasWithDifferentMenuIdAsync(menuId);
            ViewBag.MenuId = menuId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPizzaToMenu(int menuId, int pizzaId)
        {
            var success = await menuService.AddPizzaToMenuAsync(menuId, pizzaId);

            if (!success)
            {
                TempData["ErrorMessage"] = "Pizza or menu Id are not correct";
                return RedirectToAction("AddPizzas", new { menuId });
            }

            TempData["SuccessMessage"] = "Pizza successfully added in the menu";
            return RedirectToAction("AddPizzas", new { menuId });
        }

        [HttpGet]
        public async Task<IActionResult> Pizzas(int menuId)
        {
            try
            {
                var pizzas = await pizzaService.GetPizzasByMenuIdAsync(menuId);
                return View(pizzas);
            }
            catch
            {
                TempData["ErrorMessage"] = "Something went wrong";
                return RedirectToAction("All", "Menu");
            }

        }
    }
}
