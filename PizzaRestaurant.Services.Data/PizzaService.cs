namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Data.Models;
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

        public async Task AddPizzaAsync(AddPizzaViewModel model)
        {
            Dough? dough = await dbContext
                .Doughs
                .FirstOrDefaultAsync(d => d.Id == model.DoughId);

            if (dough != null)
            {
                Pizza pizza = new Pizza()
                {
                    Name = model.Name,
                    InitialPrice = model.InitialPrice,
                    ImageUrl = model.ImageUrl,
                    Description = model.Description,
                    DoughId = dough.Id
                };

                await dbContext.Pizzas.AddAsync(pizza);
                await dbContext.SaveChangesAsync();

                foreach (int productId in model.ProductsId)
                {
                    Product? product = await dbContext
                        .Product
                        .FirstOrDefaultAsync(p => p.Id == productId);

                    if (product != null)
                    {
                        PizzaProduct pizzaProduct = new PizzaProduct()
                        {
                            PizzaId = pizza.Id,
                            ProductId = productId
                        };
                        await dbContext.PizzasProducts.AddAsync(pizzaProduct);
                    }
                }

                await dbContext.SaveChangesAsync();             
            }
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

        public async Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasWithDifferentMenuIdAsync()
        {
            return await dbContext
                    .Pizzas
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

        public async Task<PizzaDetailsViewModel?> GetPizzaByIdAsync(int pizzaId)
        {
            Pizza? pizza = await dbContext.Pizzas
                .Include(p => p.PizzaProducts)
                    .ThenInclude(pp => pp.Product)
                .Include(p => p.Dough)
                .FirstOrDefaultAsync(p => p.Id == pizzaId);

            if (pizza == null)
            {
                return null;
            }

            var viewModel = new PizzaDetailsViewModel
            {
                Id = pizza.Id,
                Name = pizza.Name,
                InitialPrice = pizza.InitialPrice,
                ImageUrl = pizza.ImageUrl,
                Description = pizza.Description,
                DoughName = pizza.Dough.Name,
                Products = pizza.PizzaProducts
                                .Select(pp => new ProductsForPizzaViewModel
                                {
                                    Name = pp.Product.Name
                                })
                                .ToArray()
            };

            return viewModel;
        }

        public async Task<IEnumerable<PizzasForMenuViewModel>> GetPizzasByMenuIdAsync(int menuId)
        {
            return await dbContext
                    .Pizzas
                    .Where(p => p.MenusPizzas.Any(mp => mp.MenuId == menuId))
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
