namespace PizzaRestaurant.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    public class Menu
    {
        public Menu()
        {
            this.Pizzas = new HashSet<Pizza>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public IEnumerable<Pizza> Pizzas { get; set; } = null!;
    }
}
