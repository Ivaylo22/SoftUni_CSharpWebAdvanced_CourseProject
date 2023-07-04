namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
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
    }
}
