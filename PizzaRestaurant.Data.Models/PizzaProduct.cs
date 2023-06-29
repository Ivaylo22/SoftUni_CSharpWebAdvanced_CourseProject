using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaRestaurant.Data.Models
{
    public class PizzaProduct
    {
        [ForeignKey(nameof(Pizza))]
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; } = null!;


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
