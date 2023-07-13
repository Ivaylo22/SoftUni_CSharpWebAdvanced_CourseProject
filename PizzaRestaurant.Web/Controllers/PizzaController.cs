namespace PizzaRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Menu;
    using PizzaRestaurant.Web.ViewModels.Pizza;

    using static PizzaRestaurant.Common.NotificationMessagesConstants;

    public class PizzaController : BaseController
    {
        private readonly IPizzaService pizzaService;
        private readonly IProductService productService;
        private readonly IDoughService doughService;


        public PizzaController(
            IPizzaService _pizzaService, 
            IProductService _productService,
            IDoughService _doughService
            )
        {
            this.pizzaService = _pizzaService;
            this.productService = _productService;
            this.doughService = _doughService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<PizzasForMenuViewModel> model = await pizzaService
                .GetAllPizzasAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddPizzaViewModel model = new AddPizzaViewModel()
            {
                AvailableProducts = await productService.GetAllProductsAsync(),
                Doughs = await doughService.GetAllDoughsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPizzaViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.AvailableProducts = await productService.GetAllProductsAsync();
                model.Doughs = await doughService.GetAllDoughsAsync();
                TempData[ErrorMessage] = "Something went wrong!";

                return View(model);
            }

            try
            {
                await pizzaService.AddPizzaAsync(model);
                TempData[SuccessMessage] = "Pizza was added successfully!";

                return RedirectToAction(nameof(All));

            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house! Please try again later or contact administrator!");
                model.AvailableProducts = await productService.GetAllProductsAsync();
                model.Doughs = await doughService.GetAllDoughsAsync();

                return this.View(model);
            }    
        }

        public async Task<IActionResult> Details(int pizzaId)
        {
            try
            {
                PizzaDetailsViewModel? model = await pizzaService.GetPizzaByIdAsync(pizzaId);
                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditPizzaViewModel? model = await pizzaService.GetPizzaForEditAsync(id);

            if(model != null)
            {
                model.Doughs = await doughService.GetAllDoughsAsync();
                model.AvailableProducts = await productService.GetAllProductsAsync();
                model.ProductsId = await productService.GetProductsByPizzaIdAsync(id);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPizzaViewModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editModel);
            }

            try
            {
                await pizzaService.EditPizzaByIdAndEditModelAsync(id, editModel);
                TempData[SuccessMessage] = "Pizza is successfully edited!";

            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                editModel.Doughs = await doughService.GetAllDoughsAsync();
                editModel.AvailableProducts = await productService.GetAllProductsAsync();
                editModel.ProductsId = await productService.GetProductsByPizzaIdAsync(id);
                return View(editModel);
            }

            return RedirectToAction("All", "Pizza");
        }

    }
}
