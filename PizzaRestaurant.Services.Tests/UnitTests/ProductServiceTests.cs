namespace PizzaRestaurant.Services.Tests.UnitTests
{
    using Microsoft.EntityFrameworkCore;

    using PizzaRestaurant.Data;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Services.Data;

    using PizzaRestaurant.Web.ViewModels.Products;
    using PizzaRestaurant.Data.Models;

    using static DatabaseSeeder;

    public class ProductServiceTests
    {
        private DbContextOptions<PizzaRestaurantDbContext> dbOptions;
        private PizzaRestaurantDbContext dbContext;

        private IProductService productService;

        [SetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<PizzaRestaurantDbContext>()
                .UseInMemoryDatabase("PiizzaRestaurantInMemory" + Guid.NewGuid().ToString())
                .Options;
            dbContext = new PizzaRestaurantDbContext(dbOptions);

            dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            productService = new ProductService(dbContext);
        }

        [Test]
        public async Task AddProductAsyncShouldAddProductToDatabase()
        {
            var initialProductCount = await dbContext.Product.CountAsync();

            var productModel = new AddProductViewModel
            {
                Name = "Test Product"
            };

            await productService.AddProductAsync(productModel);

            var addedProduct = await dbContext
                .Product
                .FirstOrDefaultAsync(p => p.Name == productModel.Name);
            Assert.NotNull(addedProduct);

            var finalProductCount = await dbContext.Product.CountAsync();
            Assert.AreEqual(initialProductCount + 1, finalProductCount);
        }

        [Test]
        public async Task DeleteByIdAsyncShouldRemoveProductFromDatabase()
        {
            var product = new Product
            {
                Name = "Product to Delete"
            };
            dbContext.Product.Add(product);
            await dbContext.SaveChangesAsync();

            var initialProductCount = await dbContext.Product.CountAsync();

            await productService.DeleteByIdAsync(product.Id);

            var deletedProduct = await dbContext
                .Product
                .FirstOrDefaultAsync(p => p.Id == product.Id);
            Assert.Null(deletedProduct);

            var finalProductCount = await dbContext.Product.CountAsync();
            Assert.AreEqual(initialProductCount - 1, finalProductCount);
        }

        [Test]
        public async Task GetAllProductsAsyncShouldReturnAllProducts()
        {
            var product1 = new Product
            {
                Name = "Product 1"
            };
            var product2 = new Product
            {
                Name = "Product 2"
            };
            dbContext.Product.RemoveRange(dbContext.Product);
            dbContext.Product.AddRange(product1, product2);
            await dbContext.SaveChangesAsync();

            var products = await productService.GetAllProductsAsync();

            Assert.NotNull(products);
            Assert.AreEqual(2, products.Count());

            var productViewModel1 = products.FirstOrDefault(p => p.Name == product1.Name);
            var productViewModel2 = products.FirstOrDefault(p => p.Name == product2.Name);

            Assert.NotNull(productViewModel1);
            Assert.NotNull(productViewModel2);
        }

        [Test]
        public async Task GetProductByIdAsyncShouldReturnProductById()
        {
            var product = new Product
            {
                Name = "Test Product"
            };
            dbContext.Product.Add(product);
            await dbContext.SaveChangesAsync();

            var productViewModel = await productService.GetProductByIdAsync(product.Id);

            Assert.NotNull(productViewModel);
            Assert.AreEqual(product.Id, productViewModel.Id);
            Assert.AreEqual(product.Name, productViewModel.Name);
        }

        [Test]
        public async Task GetProductByIdAsyncShouldReturnNullForNonExistentProduct()
        {
            var productViewModel = await productService.GetProductByIdAsync(-1);

            Assert.Null(productViewModel);
        }
    }
}

