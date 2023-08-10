namespace PizzaRestaurant.Services.Tests.UnitTests
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Services.Data;

    using static DatabaseSeeder;

    public class OrderServiceTests
    {
        private DbContextOptions<PizzaRestaurantDbContext> dbOptions;
        private PizzaRestaurantDbContext dbContext;

        private IOrderService orderService;
        private ICartService cartService;

        [SetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<PizzaRestaurantDbContext>()
                .UseInMemoryDatabase("PiizzaRestaurantInMemory" + Guid.NewGuid().ToString())
                .Options;
            dbContext = new PizzaRestaurantDbContext(dbOptions);

            dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            cartService = new CartService(dbContext);
            orderService = new OrderService(dbContext, cartService);
        }

        [Test]
        public async Task AddOrderAsyncShouldAddOrderWithCorrectPrice()
        {
            var userId = "59df8a72-7c6e-4c32-9b1e-eb1d07d17f79";
            await cartService.AddPizzaToCartAsync(1, 14.99M, userId);

            await orderService.AddOrderAsync(userId);

            var order = await dbContext
                .Orders
                .FirstOrDefaultAsync();
            Assert.NotNull(order);
            Assert.AreEqual(14.99M, order.Price);
        }

        [Test]
        public async Task EmptyCartAsyncShouldRemoveCartsForUser()
        {
            var userId = "59df8a72-7c6e-4c32-9b1e-eb1d07d17f79";
            await cartService.AddPizzaToCartAsync(1, 14.99M, userId);

            await orderService.EmptyCartAsync(userId);

            var cartsCount = await dbContext
                .Carts
                .CountAsync(c => c.UserId == Guid.Parse(userId));
            Assert.AreEqual(0, cartsCount);
        }

        [Test]
        public async Task RemoveCartPizzasAsyncShouldRemoveCartPizzasForUser()
        {
            var userId = "59df8a72-7c6e-4c32-9b1e-eb1d07d17f79";
            await cartService.AddPizzaToCartAsync(1, 14.99M, userId);

            await orderService.RemoveCartPizzasAsync(userId);

            var cartPizzasCount = await dbContext
                .CartsPizzas
                .CountAsync(cp => cp.UserId == Guid.Parse(userId));
            Assert.AreEqual(0, cartPizzasCount);
        }
    }
}

