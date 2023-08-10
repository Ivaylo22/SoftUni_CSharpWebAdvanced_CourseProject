namespace PizzaRestaurant.Services.Data.Interfaces
{
    using PizzaRestaurant.Web.ViewModels.Dough;

    public interface IDoughService
    {
        Task AddDoughAsync(AddDoughViewModel model);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<DoughViewModel>> GetAllDoughsAsync();
        Task<DoughViewModel?> GetDoughByIdAsync(int doughId);
    }
}
