namespace PizzaRestaurant.Services.Data.Interfaces
{
    using PizzaRestaurant.Web.ViewModels.Menu;
    using PizzaRestaurant.Web.ViewModels.Pizza;

    public interface IMenuService
    {
        Task AddMenuAsync(AddMenuViewModel model);
        Task DeleteByIdAsync(int id);
        Task EditMenuByIdAndEditModelAsync(int id, EditMenuViewModel editModel);
        Task<IEnumerable<MenuViewModel>> GetAllMenusAsync();
        Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasByMenuIdAsync(int id);
        Task<DeleteMenuViewModel> GetMenuForDeleteAsync(int id);
        Task<EditMenuViewModel> GetMenuForEditAsync(int id);
        Task RemovePizzaFromMenuAsync(int menuId, int pizzaId);
        Task<bool> AddPizzaToMenuAsync(int menuId, int pizzaId);
        Task<RemovePizzaFromMenuViewModel> GetRemovePizzaView(int menuId, int pizzaId);
    }
}
