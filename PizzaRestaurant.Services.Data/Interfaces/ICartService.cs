namespace PizzaRestaurant.Services.Data.Interfaces
{
    using PizzaRestaurant.Web.ViewModels.Cart;

    public interface ICartService
    {
        Task AddPizzaToCartAsync(int pizzaId, decimal updatedTotalPrice, string userId);
        Task<List<CartItemViewModel>> GetAllCartItemsAsync(string userId);
        Task<decimal> GetFinalPrizeAsync(string userId);
        Task RemovePizzaFromCartAsync(int cartId, int pizzaId, string userId);
    }
}
