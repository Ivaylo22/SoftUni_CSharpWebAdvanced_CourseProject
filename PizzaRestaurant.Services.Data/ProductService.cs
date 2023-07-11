namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
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
    }
}
