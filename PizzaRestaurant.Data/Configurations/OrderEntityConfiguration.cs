namespace PizzaRestaurant.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using PizzaRestaurant.Data.Models;

    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(c => c.Price)
                .HasColumnType("decimal(18, 2)");

            builder
                .Property(o => o.OrderDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
