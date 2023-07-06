using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaRestaurant.Data.Models
{
    public class MenuPizza
    {
        [ForeignKey(nameof(Pizza))]
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; } = null!;


        [ForeignKey(nameof(Menu))]
        public int? MenuId { get; set; }
        public Menu? Menu { get; set; }
    }
}
