using PizzaRestaurant.Web.ViewModels.Topping;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface IToppingService
    {
        Task AddToppingAsync(AddToppingViewModel model);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<ToppingForPizzaVIewModel>> GetAllToppingsAsync();
        Task<ToppingForPizzaVIewModel> GetToppingByIdAsync(int toppingId);
    }
}
