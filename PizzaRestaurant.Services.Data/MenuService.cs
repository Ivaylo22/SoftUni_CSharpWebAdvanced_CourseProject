﻿namespace PizzaRestaurant.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Web.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;

    using PizzaRestaurant.Data;
    using PizzaRestaurant.Data.Models;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Menu;
    using PizzaRestaurant.Web.ViewModels.Pizza;

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
            Menu menu = await dbContext
                .Menus
                .FirstAsync(m => m.Id == id);

            if(menu != null)
            {
                menu.Name = editModel.Name;
                menu.Description = editModel.Description;

                if (editModel.SelectedPizzas != null && editModel.SelectedPizzas.Any())
                {
                    List<MenuPizza> removedMenuPizzas = menu.MenusPizzas
                        .Where(mp => editModel.SelectedPizzas.Contains(mp.PizzaId))
                        .ToList();

                    foreach (var menuPizza in removedMenuPizzas)
                    {
                        menu.MenusPizzas.Remove(menuPizza);
                    }
                }

                await this.dbContext.SaveChangesAsync();
            }
            
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

        public async Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasByMenuIdAsync(int id)
        {
            return await dbContext
                    .Pizzas
                    .Where(p => p.MenusPizzas
                    .Any(mp=> mp.MenuId == id))
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
                Id = menuToEdit.Id,
                Name = menuToEdit.Name,
                Description = menuToEdit.Description,
                MenuPizzas = await GetAllPizzasByMenuIdAsync(id)
            };
        }

        public async Task<bool> RemovePizzaFromMenuAsync(int menuId, int pizzaId)
        {
            var menu = await dbContext.Menus
                .Include(m => m.MenusPizzas)
                .FirstOrDefaultAsync(m => m.Id == menuId);

            if (menu == null)
            {
                return false;
            }

            var menuPizza = menu.MenusPizzas
                .FirstOrDefault(mp => mp.PizzaId == pizzaId);

            if (menuPizza != null)
            {
                menu.MenusPizzas.Remove(menuPizza);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> AddPizzaToMenuAsync(int menuId, int pizzaId)
        {
            var menu = await dbContext.Menus.FindAsync(menuId);
            var pizza = await dbContext.Pizzas.FindAsync(pizzaId);

            if (menu == null || pizza == null)
            {
                return false;
            }

            var menuPizza = await dbContext
                .MenusPizzas
                .FirstOrDefaultAsync(mp => mp.MenuId == menuId && mp.PizzaId == pizzaId);

            if (menuPizza != null)
            {
                return false;
            }

            menuPizza = new MenuPizza
            {
                MenuId = menuId,
                PizzaId = pizzaId
            };

            await dbContext.MenusPizzas.AddAsync(menuPizza);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
