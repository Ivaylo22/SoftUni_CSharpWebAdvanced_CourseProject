namespace PizzaRestaurant.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using PizzaRestaurant.Data.Models;

    public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .Property(c => c.FinalPrice)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
