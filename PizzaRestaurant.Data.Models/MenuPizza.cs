namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

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
