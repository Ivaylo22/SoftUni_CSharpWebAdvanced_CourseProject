namespace PizzaRestaurant.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using PizzaRestaurant.Data.Models;
    public class PizzaProductConfiguration : IEntityTypeConfiguration<PizzaProduct>
    {
        public void Configure(EntityTypeBuilder<PizzaProduct> builder)
        {
            builder.HasKey(pp => new { pp.PizzaId, pp.ProductId });

            builder.HasOne(pp => pp.Pizza)
                .WithMany(p => p.PizzaProducts)
                .HasForeignKey(pp => pp.PizzaId);

            builder.HasOne(pp => pp.Product)
                .WithMany(p => p.PizzaProducts)
                .HasForeignKey(pp => pp.ProductId);

            builder.HasData(this.GeneratePizzaProducts());
        }

        private PizzaProduct[] GeneratePizzaProducts()
        {
            ICollection<PizzaProduct> pizzaProducts = new HashSet<PizzaProduct>();

            PizzaProduct pizzaProduct;

            pizzaProduct = new PizzaProduct()
            {
                PizzaId = 1,
                ProductId = 1,
            };

            pizzaProducts.Add(pizzaProduct);

            pizzaProduct = new PizzaProduct()
            {
                PizzaId = 1,
                ProductId = 2,
            };

            pizzaProducts.Add(pizzaProduct);

            pizzaProduct = new PizzaProduct()
            {
                PizzaId = 1,
                ProductId = 3,
            };

            pizzaProducts.Add(pizzaProduct);

            pizzaProduct = new PizzaProduct()
            {
                PizzaId = 2,
                ProductId = 3,
            };

            pizzaProducts.Add(pizzaProduct);

            pizzaProduct = new PizzaProduct()
            {
                PizzaId = 2,
                ProductId = 4,
            };

            pizzaProducts.Add(pizzaProduct);

            pizzaProduct = new PizzaProduct()
            {
                PizzaId = 2,
                ProductId = 5,
            };


            pizzaProducts.Add(pizzaProduct);

            return pizzaProducts.ToArray();
        }
    }
}
