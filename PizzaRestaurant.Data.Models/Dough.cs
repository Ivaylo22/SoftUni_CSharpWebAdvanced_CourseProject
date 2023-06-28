namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PizzaRestaurant.Common.EntityValidationsConstants.Dough;

    public class Dough
    {
        public Dough()
        {
            this.Pizzas = new HashSet<Pizza>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<Pizza> Pizzas { get; set; }
    }
}
