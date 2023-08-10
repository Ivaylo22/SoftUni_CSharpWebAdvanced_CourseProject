namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class PizzaProduct
    {
        [ForeignKey(nameof(Pizza))]
        public int? PizzaId { get; set; }
        public Pizza? Pizza { get; set; }


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
