﻿namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Data.Models;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Products;
    using System.Collections.Generic;

    public class ProductService : IProductService
    {
        private readonly PizzaRestaurantDbContext dbContext;

        public ProductService(PizzaRestaurantDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task AddProductAsync(AddProductViewModel model)
        {
            Product product = new Product()
            {
                Name = model.Name
            };
            
            await this.dbContext.Product.AddAsync(product);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            Product productToDelete = await dbContext
                .Product
                .FirstAsync(x => x.Id == id);

            dbContext.Remove(productToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductsForPizzaViewModel>> GetAllProductsAsync()
        {
            return await dbContext
                .Product
                .Select(p => new ProductsForPizzaViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToArrayAsync();
        }

        public async Task<ProductsForPizzaViewModel> GetProductByIdAsync(int productId)
        {
            Product? product = await this.dbContext
                .Product
                .FirstOrDefaultAsync(p => p.Id == productId);

            if(product == null)
            {
                return null;
            }

            ProductsForPizzaViewModel viewModel = new ProductsForPizzaViewModel
            {
                Id = productId,
                Name = product.Name
            };

            return viewModel;
        }

        public async Task<IEnumerable<int>> GetProductsByPizzaIdAsync(int pizzaId)
        {
            return await this.dbContext
                .PizzasProducts
                .Where(pp => pp.PizzaId == pizzaId)
                .Select(pp => pp.ProductId)
                .ToArrayAsync();
        }
    }
}
