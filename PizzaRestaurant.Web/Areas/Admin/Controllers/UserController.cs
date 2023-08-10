namespace PizzaRestaurant.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Web.ViewModels.User;

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
