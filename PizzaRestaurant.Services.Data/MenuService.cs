namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Data.Models;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Menu;
    using PizzaRestaurant.Web.ViewModels.Pizza;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Web.ViewModels.Products;

    public class MenuService : IMenuService
    {
        private readonly PizzaRestaurantDbContext dbContext;

        public MenuService(PizzaRestaurantDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task AddMenuAsync(AddMenuViewModel model)
        {
            Menu newMenu = new Menu()
            {
                Name = model.Name,
                Description = model.Description
            };

            await dbContext.Menus.AddAsync(newMenu);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            Menu menuToDelete = await this.dbContext
                .Menus
                .FirstAsync(m => m.Id == id);

            this.dbContext.Menus.Remove(menuToDelete);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditMenuByIdAndEditModelAsync(int id, EditMenuViewModel editModel)
        {
            //IEnumerable<Pizza> allPizzas = await dbContext
            //    .Pizzas
            //    .Where(p => p.)

            //Menu menu = await dbContext
            //    .Menus
            //    .FirstAsync(m => m.Id == id);

            //menu.Name = editModel.Name;
            //menu.Description = editModel.Description;
            //menu.Pizzas = editModel.MenuPizzas
        }

        public async Task<IEnumerable<MenuViewModel>> GetAllMenusAsync()
        {
            var menus = await dbContext
                .Menus
                .Select (m => new MenuViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description
                })
                .ToListAsync ();

            return menus;
        }

        public async Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasAsync(int id)
        {
            return await dbContext
                    .Pizzas
                    .Where(p => p.Id == id)
                    .Select(p => new PizzasForMenuViewModel
                    {
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

        public async Task<DeleteMenuViewModel> GetMenuForDeleteAsync(int id)
        {
            Menu menuToDelete = await dbContext
                .Menus
                .FirstAsync (m => m.Id == id);

            return new DeleteMenuViewModel()
            {
                Name = menuToDelete.Name,
                Description = menuToDelete.Description
            };
        }

        public async Task<EditMenuViewModel> GetMenuForEditAsync(int id)
        {
            Menu menuToEdit = await dbContext
                .Menus
                .FirstAsync(m => m.Id == id);

            return new EditMenuViewModel()
            {
                Name = menuToEdit.Name,
                Description = menuToEdit.Description,
                MenuPizzas = await GetAllPizzasAsync(id)
            };
        }
    }
}
