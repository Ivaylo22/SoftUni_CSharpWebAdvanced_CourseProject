﻿using PizzaRestaurant.Web.ViewModels.Menu;
using PizzaRestaurant.Web.ViewModels.Pizza;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface IPizzaService
    {
        Task AddPizzaAsync(AddPizzaViewModel model);
        Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasWithDifferentMenuIdAsync(int id);
        Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasAsync();
        Task<PizzaDetailsViewModel?> GetPizzaByIdAsync(int pizzaId);
        Task<EditPizzaViewModel?> GetPizzaForEditAsync(int id);
        Task<IEnumerable<PizzasForMenuViewModel>> GetPizzasByMenuIdAsync(int menuId);
        Task EditPizzaByIdAndEditModelAsync(int id, EditPizzaViewModel editModel);
        Task<DeletePizzaViewModel> GetPizzaForDeleteAsync(int id);
        Task DeleteByIdAsync(int id);
        Task<OrderPizzaViewModel?> GetPizzaForOrderAsync(int pizzaId);
    }
}
