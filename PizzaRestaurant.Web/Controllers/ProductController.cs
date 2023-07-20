namespace PizzaRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaRestaurant.Services.Data;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Menu;
    using PizzaRestaurant.Web.ViewModels.Pizza;
    using PizzaRestaurant.Web.ViewModels.Products;

    using static PizzaRestaurant.Common.NotificationMessagesConstants;

    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
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
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.AddProductAsync(model);

            return RedirectToAction(nameof(Options));
        }

        [HttpGet]
        public async Task<IActionResult> Remove()
        {
            IEnumerable<ProductsForPizzaViewModel> model = await productService.GetAllProductsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            try
            {
                ProductsForPizzaViewModel model =
                    await productService.GetProductByIdAsync(productId);
                return View(model);

            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                return RedirectToAction("Options", "Product");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProduct(int id, ProductsForPizzaViewModel productModel)
        {
            try
            {
                await productService.DeleteByIdAsync(id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occured. Try later or contact administrator!";
                return View(productModel);
            }

            TempData[SuccessMessage] = "Product is deleted successfully";
            return RedirectToAction("Options", "Product");
        }
    }
}
