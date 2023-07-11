using PizzaRestaurant.Web.ViewModels.Products;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsForPizzaViewModel>> GetAllProductsAsync();
    }
}
