namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CartPizza
    {
        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart Cart { get; set; }


        [ForeignKey(nameof(Pizza))]
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        public decimal UpdatedPrice { get; set; }
    }
}
