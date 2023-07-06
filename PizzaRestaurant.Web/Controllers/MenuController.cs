namespace PizzaRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Menu;

    using static PizzaRestaurant.Common.NotificationMessagesConstants;
    public class MenuController : BaseController
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService _menuService)
        {
            menuService = _menuService;
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
    }
}
