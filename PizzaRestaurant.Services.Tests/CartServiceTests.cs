﻿namespace PizzaRestaurant.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Services.Data;

    using PizzaRestaurant.Data.Models;

    using static DatabaseSeeder;

    public class CartServiceTests
    {
        private DbContextOptions<PizzaRestaurantDbContext> dbOptions;
        private PizzaRestaurantDbContext dbContext;

        private ICartService cartService;

        [SetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<PizzaRestaurantDbContext>()
                .UseInMemoryDatabase("PiizzaRestaurantInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new PizzaRestaurantDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.cartService = new CartService(this.dbContext);
        }

        [Test]
        public async Task AddPizzaToCartAsyncShouldAddPizzaToCart()
        {
            var userId = "12345678-1234-1234-1234-123456789012";
            var pizzaForCart = new Pizza
            {
                Name = "Pizza for Cart",
                InitialPrice = 10.99M,
                ImageUrl = "cart-image-url",
                Description = "Cart description",
                DoughId = DoughTest.Id
            };
            dbContext.Pizzas.Add(pizzaForCart);
            await dbContext.SaveChangesAsync();
            var updatedTotalPrice = 15.99M;

            await cartService.AddPizzaToCartAsync(pizzaForCart.Id, updatedTotalPrice, userId);

            var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.UserId == Guid.Parse(userId));
            Assert.NotNull(cart);
            Assert.AreEqual(updatedTotalPrice, cart.FinalPrice);

            var cartPizza = await dbContext.CartsPizzas.FirstOrDefaultAsync(cp => cp.CartId == cart.Id && cp.PizzaId == pizzaForCart.Id);
            Assert.NotNull(cartPizza);
            Assert.AreEqual(cart.Id, cartPizza.CartId);
            Assert.AreEqual(pizzaForCart.Id, cartPizza.PizzaId);
            Assert.AreEqual(Guid.Parse(userId), cartPizza.UserId);
            Assert.AreEqual(updatedTotalPrice, cartPizza.UpdatedPrice);
        }

        [Test]
        public async Task GetAllCartItemsAsyncShouldReturnAllCartItems()
        {
            var userId = "12345678-1234-1234-1234-123456789012"; 
            var cart = new Cart
            {
                UserId = Guid.Parse(userId),
                FinalPrice = 0.0M
            };
            dbContext.Carts.Add(cart);
            await dbContext.SaveChangesAsync();

            var pizza = new Pizza
            {
                Name = "Test Pizza",
                InitialPrice = 10.0M,
                ImageUrl = "test-image-url",
                Description = "Test description",
                DoughId = DoughTest.Id
            };
            dbContext.Pizzas.Add(pizza);
            await dbContext.SaveChangesAsync();

            var cartPizza = new CartPizza
            {
                Cart = cart,
                Pizza = pizza,
                UserId = Guid.Parse(userId),
                UpdatedPrice = 10.0M
            };
            dbContext.CartsPizzas.Add(cartPizza);
            await dbContext.SaveChangesAsync();

            var cartItems = await cartService.GetAllCartItemsAsync(userId);

            Assert.NotNull(cartItems);
            Assert.IsTrue(cartItems.Any());

            var cartItem = cartItems.First();
            Assert.AreEqual(Guid.Parse(userId), cartItem.UserId);
            Assert.AreEqual(pizza.Id, cartItem.PizzaId);
            Assert.AreEqual(cart.Id, cartItem.CartId);
            Assert.AreEqual(pizza.Name, cartItem.PizzaName);
            Assert.AreEqual(cartPizza.UpdatedPrice, cartItem.Price);
        }

        [Test]
        public async Task GetFinalPrizeAsyncShouldReturnCorrectFinalPrice()
        {
            var userId = "12345678-1234-1234-1234-123456789012"; 
            var cart = new Cart
            {
                UserId = Guid.Parse(userId),
                FinalPrice = 0.0M
            };
            dbContext.Carts.Add(cart);
            await dbContext.SaveChangesAsync();

            var pizza1 = new Pizza
            {
                Name = "Pizza 1",
                InitialPrice = 10.0M,
                ImageUrl = "image-url-1",
                Description = "Description 1",
                DoughId = DoughTest.Id
            };
            dbContext.Pizzas.Add(pizza1);
            await dbContext.SaveChangesAsync();

            var cartPizza1 = new CartPizza
            {
                Cart = cart,
                Pizza = pizza1,
                UserId = Guid.Parse(userId),
                UpdatedPrice = 10.0M
            };
            dbContext.CartsPizzas.Add(cartPizza1);
            await dbContext.SaveChangesAsync();

            var pizza2 = new Pizza
            {
                Name = "Pizza 2",
                InitialPrice = 15.0M,
                ImageUrl = "image-url-2",
                Description = "Description 2",
                DoughId = DoughTest.Id
            };
            dbContext.Pizzas.Add(pizza2);
            await dbContext.SaveChangesAsync();

            var cartPizza2 = new CartPizza
            {
                Cart = cart,
                Pizza = pizza2,
                UserId = Guid.Parse(userId),
                UpdatedPrice = 15.0M
            };
            dbContext.CartsPizzas.Add(cartPizza2);
            await dbContext.SaveChangesAsync();

            var finalPrice = await cartService.GetFinalPrizeAsync(userId);

            Assert.AreEqual(pizza1.InitialPrice + pizza2.InitialPrice, finalPrice);
        }

        [Test]
        public async Task RemovePizzaFromCartAsyncShouldRemovePizzaFromCart()
        {
            var userId = "12345678-1234-1234-1234-123456789012"; 
            var cart = new Cart
            {
                UserId = Guid.Parse(userId),
                FinalPrice = 0.0M
            };
            dbContext.Carts.Add(cart);
            await dbContext.SaveChangesAsync();

            var pizza = new Pizza
            {
                Name = "Pizza",
                InitialPrice = 10.0M,
                ImageUrl = "image-url",
                Description = "Description",
                DoughId = DoughTest.Id
            };
            dbContext.Pizzas.Add(pizza);
            await dbContext.SaveChangesAsync();

            var cartPizza = new CartPizza
            {
                Cart = cart,
                Pizza = pizza,
                UserId = Guid.Parse(userId),
                UpdatedPrice = 10.0M
            };
            dbContext.CartsPizzas.Add(cartPizza);
            await dbContext.SaveChangesAsync();

            await cartService.RemovePizzaFromCartAsync(cart.Id, pizza.Id, userId);

            var removedCartPizza = await dbContext.CartsPizzas.FirstOrDefaultAsync(cp => cp.CartId == cart.Id && cp.PizzaId == pizza.Id);
            Assert.Null(removedCartPizza);
        }
    }
}
