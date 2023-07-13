using PizzaRestaurant.Web.ViewModels.Dough;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface IDoughService
    {
        Task<IEnumerable<DoughViewModel>> GetAllDoughsAsync();
        Task<DoughViewModel?> GetDoughByIdAsync(int doughId);
    }
}
