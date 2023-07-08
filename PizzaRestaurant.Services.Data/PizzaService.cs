namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Pizza;
    using PizzaRestaurant.Web.ViewModels.Products;

    public class PizzaService : IPizzaService
    {
        private readonly PizzaRestaurantDbContext dbContext;

        public PizzaService(PizzaRestaurantDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasWithDifferentMenuIdAsync(int id)
        {
            return await dbContext
                    .Pizzas
                    .Where(p => !p.MenusPizzas.Any(mp => mp.MenuId == id))
                    .Select(p => new PizzasForMenuViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        InitialPrice = p.InitialPrice,
                        ImageUrl = p.ImageUrl,
                        Description = p.Description,
                        DoughName = p.Dough.Name,
                        Products = p.PizzaProducts
                            .Select(pp => new ProductsForPizzaViewModel
                            {
                                Name = pp.Product.Name
                            })
                            .ToArray()
                    })
                    .ToArrayAsync();
        }
    }
}
