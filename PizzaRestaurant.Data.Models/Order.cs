namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;
    }
}
