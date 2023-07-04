namespace PizzaRestaurant.Services.Data.Interfaces
{
    using PizzaRestaurant.Web.ViewModels.Menu;

    public interface IMenuService
    {
        Task AddMenuAsync(AddMenuViewModel model);
        Task<IEnumerable<MenuViewModel>> GetAllMenusAsync();

    }
}
