namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static PizzaRestaurant.Common.EntityValidationsConstants.Pizza;

    public class Pizza
    {
        public Pizza()
        {
            this.PizzaProducts = new HashSet<PizzaProduct>();
            this.Toppings = new HashSet<Topping>();
            this.Carts = new HashSet<Cart>();
            this.MenusPizzas = new HashSet<MenuPizza>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public decimal InitialPrice { get; set; }


        [Required]
        [MaxLength(ImageMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Dough))]
        public int DoughId { get; set; }

        public Dough Dough { get; set; } = null!;

        public ICollection<MenuPizza>? MenusPizzas { get; set; }

        public ICollection<PizzaProduct> PizzaProducts { get; set; }

        public ICollection<Topping>? Toppings { get; set; }

        public ICollection<Cart>? Carts { get; set; }
    }
}