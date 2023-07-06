namespace PizzaRestaurant.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using PizzaRestaurant.Data.Models;

    public class MenuEntityConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasData(this.GenerateProducts());
        }

        private Menu[] GenerateProducts()
        {
            ICollection<Menu> menus = new HashSet<Menu>();

            Menu menu;

            menu = new Menu()
            {
                Id = 1,
                Name = "Breakfast menu",
                Description = "Gotino menu"
            };

            menus.Add(menu);

            return menus.ToArray();
        }
    }
}
