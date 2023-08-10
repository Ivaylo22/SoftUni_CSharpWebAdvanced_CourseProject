namespace PizzaRestaurant.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using PizzaRestaurant.Data.Models;

    public class CartPizzaConfiguration : IEntityTypeConfiguration<CartPizza>
    {
        public void Configure(EntityTypeBuilder<CartPizza> builder)
        {
            builder.HasKey(cp => new { cp.CartId, cp.PizzaId });

            builder.HasOne(cp => cp.Pizza)
                .WithMany(p => p.CartsPizzas)
                .HasForeignKey(cp => cp.PizzaId);

            builder.HasOne(cp => cp.Cart)
                .WithMany(c => c.CartsPizzas)
                .HasForeignKey(cp => cp.CartId);

            builder
                .Property(p => p.UpdatedPrice)
                .HasColumnType("decimal(18, 2)");

        }
    }
}
