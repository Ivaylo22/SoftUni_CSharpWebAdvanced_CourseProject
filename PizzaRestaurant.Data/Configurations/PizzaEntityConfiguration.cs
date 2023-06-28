namespace PizzaRestaurant.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using PizzaRestaurant.Data.Models;
    using System.Reflection.Emit;

    public class PizzaEntityConfiguration : IEntityTypeConfiguration<Pizza>
    {

        public void Configure(EntityTypeBuilder<Pizza> builder)
        {
            builder
                .Property(p => p.InitialPrice)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
