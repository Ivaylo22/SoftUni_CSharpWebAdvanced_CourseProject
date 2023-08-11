namespace PizzaRestaurant.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Dough;

    using static PizzaRestaurant.Common.NotificationMessagesConstants;


    public class DoughController : BaseAdminController
    {
        private readonly IDoughService doughService;

        public DoughController(IDoughService _doughService)
        {
            doughService = _doughService;
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
        public async Task<IActionResult> Add(AddDoughViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Add), model);
            }

            await doughService.AddDoughAsync(model);

            return RedirectToAction(nameof(Options));
        }

        [HttpGet]
        public async Task<IActionResult> Remove()
        {
            IEnumerable<DoughViewModel> model =
                await doughService.GetAllDoughsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveDough(int doughId)
        {
            try
            {
                DoughViewModel model = await doughService.GetDoughByIdAsync(doughId);
                if (model == null)
                {
                    return RedirectToAction("Error404", "Home");
                }

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred. Try later or contact administrator!";
                return RedirectToAction("Options", "Dough");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDough(int id, DoughViewModel doughModel)
        {
            try
            {
                await doughService.DeleteByIdAsync(id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred. Try later or contact administrator!";
                return View(doughModel);
            }

            TempData[SuccessMessage] = "Dough is deleted successfully";
            return RedirectToAction("Options", "Dough");
        }
    }
}
