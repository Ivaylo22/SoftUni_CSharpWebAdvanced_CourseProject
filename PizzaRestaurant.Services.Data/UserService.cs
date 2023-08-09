namespace PizzaRestaurant.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly PizzaRestaurantDbContext dbContext;

        public UserService(PizzaRestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
           
        List<UserViewModel> allUsers = await this.dbContext
                   .Users
                   .Select(u => new UserViewModel()
                   {
                       Id = u.Id.ToString(),
                       Email = u.Email,
                   })
                   .ToListAsync();

                return allUsers;
        }
    }
}
