namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cart
    {
        public Cart()
        {
            this.Pizzas = new HashSet<Pizza>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public ICollection<Pizza>? Pizzas { get; set; }

        [Required]
        public decimal FinalPrice { get; set; }
    }
}
