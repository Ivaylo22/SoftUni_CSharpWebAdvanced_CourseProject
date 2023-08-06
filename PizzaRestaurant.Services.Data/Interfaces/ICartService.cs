using PizzaRestaurant.Web.ViewModels.Cart;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface ICartService
    {
        Task AddPizzaToCartAsync(int pizzaId, decimal updatedTotalPrice, string userId);
        Task<List<CartItemViewModel>> GetAllCartItemsAsync(string userId);
        Task<decimal> GetFinalPrizeAsync(string userId);
    }
}
