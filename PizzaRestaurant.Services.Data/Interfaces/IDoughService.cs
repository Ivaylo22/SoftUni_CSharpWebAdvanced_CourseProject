using PizzaRestaurant.Web.ViewModels.Dough;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface IDoughService
    {
        Task<IEnumerable<DoughForAddPizzaViewModel>> GetAllDoughsAsync();
    }
}
