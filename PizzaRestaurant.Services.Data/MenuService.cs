namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Data.Models;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Menu;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
    }
}
