namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.Dough;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DoughService : IDoughService
    {
        private readonly PizzaRestaurantDbContext dbContext;

        public DoughService(PizzaRestaurantDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public async Task<IEnumerable<DoughViewModel>> GetAllDoughsAsync()
        {
            return await dbContext
                .Doughs
                .Select(d => new DoughViewModel
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToArrayAsync();
        }

        public async Task<DoughViewModel?> GetDoughByIdAsync(int doughId)
        {
            var dough = await dbContext
                .Doughs
                .FirstOrDefaultAsync(d => d.Id == doughId);

            if (dough != null)
            {
                return new DoughViewModel
                {
                    Id = dough.Id,
                    Name = dough.Name
                };
            }

            return null;
        }
    }
}
