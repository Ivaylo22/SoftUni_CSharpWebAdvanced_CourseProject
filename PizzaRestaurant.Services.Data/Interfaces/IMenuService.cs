namespace PizzaRestaurant.Services.Data.Interfaces
{
    using PizzaRestaurant.Web.ViewModels.Menu;

    public interface IMenuService
    {
        Task<IEnumerable<MenuViewModel>> GetAllMenusAsync();

    }
}
