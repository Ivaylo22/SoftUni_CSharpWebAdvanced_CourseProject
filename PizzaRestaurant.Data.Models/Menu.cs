namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PizzaRestaurant.Common.EntityValidationsConstants.Menu;

    public class Menu
    {
        public Menu()
        {
            this.Pizzas = new HashSet<Pizza>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public IEnumerable<Pizza> Pizzas { get; set; } = null!;
    }
}
