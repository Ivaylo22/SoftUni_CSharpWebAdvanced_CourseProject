namespace PizzaRestaurant.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using System;

    using PizzaRestaurant.Data;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Services.Data;

    using static DatabaseSeeder;
    using PizzaRestaurant.Web.ViewModels.Topping;
    using PizzaRestaurant.Data.Models;

    public class ToppingServiceTests
    {
        private DbContextOptions<PizzaRestaurantDbContext> dbOptions;
        private PizzaRestaurantDbContext dbContext;

        private IToppingService toppingService;

        [SetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<PizzaRestaurantDbContext>()
                .UseInMemoryDatabase("PiizzaRestaurantInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new PizzaRestaurantDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.toppingService = new ToppingService(this.dbContext);
        }
        [Test]
        public async Task AddToppingAsyncShouldAddToppingAndIncreaseCount()
        {
            var model = new AddToppingViewModel
            {
                Name = "New Topping",
                Price = 2.5M
            };

            var initialCount = await dbContext.Toppings.CountAsync();
            await toppingService.AddToppingAsync(model);
            var newCount = await dbContext.Toppings.CountAsync();

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public async Task DeleteByIdAsyncShouldDeleteToppingAndDecreaseCount()
        {
            var topping = new Topping
            {
                Name = "Topping to Delete",
                Price = 3.0M
            };
            dbContext.Toppings.Add(topping);
            await dbContext.SaveChangesAsync();

            var initialCount = await dbContext.Toppings.CountAsync();
            await toppingService.DeleteByIdAsync(topping.Id);
            var newCount = await dbContext.Toppings.CountAsync();

            Assert.AreEqual(initialCount - 1, newCount);
        }


        [Test]
        public async Task GetToppingByIdAsyncShouldReturnCorrectTopping()
        {
            var topping = new Topping { Name = "Test Topping", Price = 2.0M };
            dbContext.Toppings.Add(topping);
            await dbContext.SaveChangesAsync();

            var retrievedTopping = await toppingService.GetToppingByIdAsync(topping.Id);

            Assert.NotNull(retrievedTopping);
            Assert.AreEqual(topping.Name, retrievedTopping.Name);
            Assert.AreEqual(topping.Price, retrievedTopping.Price);
        }

    }
}
