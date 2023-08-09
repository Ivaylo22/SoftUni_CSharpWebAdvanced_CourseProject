using PizzaRestaurant.Web.ViewModels.Dough;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface IDoughService
    {
        Task AddDoughAsync(AddDoughViewModel model);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<DoughViewModel>> GetAllDoughsAsync();
        Task<DoughViewModel?> GetDoughByIdAsync(int doughId);
    }
}
