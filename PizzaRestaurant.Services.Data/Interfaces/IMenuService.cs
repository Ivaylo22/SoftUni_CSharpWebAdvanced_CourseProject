﻿namespace PizzaRestaurant.Services.Data.Interfaces
{
    using PizzaRestaurant.Web.ViewModels.Menu;
    using PizzaRestaurant.Web.ViewModels.Pizza;

    public interface IMenuService
    {
        Task AddMenuAsync(AddMenuViewModel model);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<MenuViewModel>> GetAllMenusAsync();
        Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasAsync(int id);
        Task<DeleteMenuViewModel> GetMenuForDeleteAsync(int id);
    }
}
