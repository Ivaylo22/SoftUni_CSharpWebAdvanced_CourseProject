﻿namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Data.Models;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Cart;
    using PizzaRestaurant.Web.ViewModels.Pizza;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CartService : ICartService
    {
        private readonly PizzaRestaurantDbContext dbContext;

        public CartService(PizzaRestaurantDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public async Task AddPizzaToCartAsync(int pizzaId, decimal updatedTotalPrice, string userId)
        {
            Cart cart = new Cart()
            {
                UserId = Guid.Parse(userId),
                FinalPrice = updatedTotalPrice,              
            };

            await dbContext.Carts.AddAsync(cart);
            await dbContext.SaveChangesAsync();

            Pizza? pizza = await dbContext
                .Pizzas
                .FirstOrDefaultAsync(p => p.Id == pizzaId);

            if(pizza != null)
            {
                CartPizza cartPizza = new CartPizza()
                {
                    Cart = cart,
                    Pizza = pizza,
                    UserId = Guid.Parse(userId),
                    UpdatedPrice = updatedTotalPrice
                };

                await dbContext.CartsPizzas.AddAsync(cartPizza);
                await dbContext.SaveChangesAsync();
            }

        }

        public async Task<List<CartItemViewModel>> GetAllCartItemsAsync(string userId)
        {
            return await dbContext
                .CartsPizzas
                .Where(cp => cp.UserId == Guid.Parse(userId))
                .Select(cp => new CartItemViewModel
                {
                    UserId = Guid.Parse(userId),
                    PizzaId = cp.PizzaId,
                    CartId = cp.CartId,
                    PizzaName = cp.Pizza.Name,
                    Price = cp.UpdatedPrice
                })
                .ToListAsync();
        }

        public async Task<decimal> GetFinalPrizeAsync(string userId)
        {
            return await dbContext
                .CartsPizzas
                .Where(c => c.UserId == Guid.Parse(userId))
                .Select(cp => cp.UpdatedPrice)
                .SumAsync();
        }

        public async Task RemovePizzaFromCartAsync(int cartId, int pizzaId, string userId)
        {
            CartPizza? cartPizza = await dbContext 
                .CartsPizzas
                .Where(cp => cp.CartId == cartId && cp.PizzaId == pizzaId && cp.UserId == Guid.Parse(userId))
                .FirstOrDefaultAsync();

            if(cartPizza != null)
            {
                dbContext.CartsPizzas.Remove(cartPizza);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
