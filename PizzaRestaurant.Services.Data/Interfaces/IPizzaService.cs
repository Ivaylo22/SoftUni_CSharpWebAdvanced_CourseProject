using PizzaRestaurant.Web.ViewModels.Pizza;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface IPizzaService
    {
        Task AddPizzaAsync(AddPizzaViewModel model);
        Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasWithDifferentMenuIdAsync(int id);
        Task<PizzaDetailsViewModel?> GetPizzaByIdAsync(int pizzaId);
        Task<IEnumerable<PizzasForMenuViewModel>> GetPizzasByMenuIdAsync(int menuId);
    }
}
