using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PizzaRestaurant.Services.Data.Interfaces;
using PizzaRestaurant.Web.ViewModels.User;

namespace PizzaRestaurant.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            this.userService = _userService;
        }

        [Route("User/All")]
        public async Task<IActionResult> All()
        {
                IEnumerable<UserViewModel> users = await this.userService.AllAsync();

            return View(users);
        }
    }
}
