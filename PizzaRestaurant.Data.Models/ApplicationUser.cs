using Microsoft.AspNetCore.Identity;

namespace PizzaRestaurant.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Orders = new HashSet<Order>();
        }

        public ICollection<Order> Orders { get; set; }
    }
}
