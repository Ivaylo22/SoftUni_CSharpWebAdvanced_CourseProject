namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PizzaRestaurant.Common.EntityValidationsConstants.Menu;

    public class Menu
    {
        public Menu()
        {
            this.MenusPizzas = new HashSet<MenuPizza>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public ICollection<MenuPizza>? MenusPizzas { get; set; }
    }
}
