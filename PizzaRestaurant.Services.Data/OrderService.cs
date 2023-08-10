namespace PizzaRestaurant.Services.Data 
{ 

    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using PizzaRestaurant.Data;
    using PizzaRestaurant.Data.Models;
    using PizzaRestaurant.Services.Data.Interfaces;

    public class OrderService : IOrderService
    {
        private readonly PizzaRestaurantDbContext dbContext;
        private readonly ICartService cartService;

        public OrderService(PizzaRestaurantDbContext _dbContext, ICartService _cartService)
        {
            this.dbContext = _dbContext;
            this.cartService = _cartService;
        }

        public async Task AddOrderAsync(string userId)
        {
            decimal price = await cartService.GetFinalPrizeAsync(userId);

            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                UserId = Guid.Parse(userId),
                Price = price
            };

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task EmptyCartAsync(string userId)
        {
            List<Cart> carts = await dbContext
                .Carts
                .Where(c => c.UserId == Guid.Parse(userId) )
                .ToListAsync();

            dbContext.Carts.RemoveRange(carts);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveCartPizzasAsync(string userId)
        {
            List<CartPizza> cartPizzas = await dbContext
                .CartsPizzas
                .Where(cp => cp.UserId == Guid.Parse(userId))
                .ToListAsync();

            dbContext.CartsPizzas.RemoveRange(cartPizzas);
            await dbContext.SaveChangesAsync();
                
        }
    }
}
