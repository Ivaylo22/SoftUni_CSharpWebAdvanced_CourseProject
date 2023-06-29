namespace PizzaRestaurant.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using PizzaRestaurant.Data.Models;

    public class PizzaEntityConfiguration : IEntityTypeConfiguration<Pizza>
    {

        public void Configure(EntityTypeBuilder<Pizza> builder)
        {
            builder
                .Property(p => p.InitialPrice)
                .HasColumnType("decimal(18, 2)");

            builder.HasData(this.GeneratePizzas());
        }

        private Pizza[] GeneratePizzas()
        {
            ICollection<Pizza> pizzas = new HashSet<Pizza>();

            Pizza pizza;

            pizza = new Pizza()
            {
                Id = 1,
                Name = "Margherita",
                InitialPrice = 10.99m,
                ImageUrl = "https://drive.google.com/file/d/1iSiFsUSFdY_1CCIL8J6tJLzNS8k-USd0/view?usp=sharing",
                Description = "Classic pizza with tomato sauce and mozzarella cheese",
                DoughId = 1
            };

            pizzas.Add(pizza);

            pizza = new Pizza()
            {
                Id = 2,
                Name = "Pepperoni",
                InitialPrice = 12.99m,
                ImageUrl = "https://drive.google.com/file/d/1XZNR8QzYuP_6R2jAmgGtPzkphCjIJr1E/view?usp=sharing",
                Description = "Traditional pizza topped with tomato sauce and slices of pepperoni.",
                DoughId = 2
            };

            pizzas.Add(pizza);

            return pizzas.ToArray();
        }
    }
}
