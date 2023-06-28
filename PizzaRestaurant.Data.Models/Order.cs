namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public Cart Cart { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;
    }
}
