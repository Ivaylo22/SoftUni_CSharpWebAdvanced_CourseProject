namespace PizzaRestaurant.Services.Data.Interfaces
{
    using PizzaRestaurant.Web.ViewModels.Menu;

    public interface IMenuService
    {
        Task AddMenuAsync(AddMenuViewModel model);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<MenuViewModel>> GetAllMenusAsync();
        Task<DeleteMenuViewModel> GetMenuForDeleteAsync(int id);
    }
}
