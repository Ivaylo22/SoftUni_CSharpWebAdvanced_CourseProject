
namespace PizzaRestaurant.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PizzaRestaurant.Data.Configurations;
    using PizzaRestaurant.Data.Models;

    public class PizzaRestaurantDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public PizzaRestaurantDbContext(DbContextOptions<PizzaRestaurantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; } = null!;

        public DbSet<Dough> Doughs { get; set; } = null!;

        public DbSet<Menu> Menus { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Pizza> Pizzas { get; set; } = null!;

        public DbSet<Product> Product { get; set; } = null!;

        public DbSet<Topping> Toppings { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityConfigurationHelper.ApplyEntityConfigurations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}